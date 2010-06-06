using System;
using System.Net;
using System.Xml;

namespace SABSync
{
    public interface ISabService
    {
        string AddByUrl(NzbInfo nzb);
        bool IsInQueue(Episode episode);
        //bool IsInHistory(string rssTitle, string rssTitleFix);
    }

    public class SabService : ISabService
    {
        private static readonly Logger Logger = new Logger();

        public SabService()
        {
            Config = new Config();
        }

        private Config Config { get; set; }

        #region ISabService Members

        public string AddByUrl(NzbInfo nzb)
        {
            // TODO: create an sab action type
            const string mode = "addurl";
            // TODO: use HttpUtility.UrlEncode once moved to dll
            string name = nzb.Link.ToString().Replace("&", "%26");
            const string cat = "tv";
            string nzbname = CleanUrlString(CleanString(nzb.Title));
            string action = string.Format("mode={0}&name={1}&cat={2}&nzbname={3}", mode, name, cat, nzbname);

            string request = string.Format(Config.SabRequest, action);
            Logger.Log("Adding report [{0}] to the queue.", nzb.Title);

            return SendRequest(request);
        }

        #endregion

        // TODO: refactor
        #region 

        public bool IsInQueue(Episode episode)
        {
            string rssTitle = episode.FeedItem.Title;
            string rssTitleFix = episode.FeedItem.TitleFix;
            string nzbId = episode.FeedItem.NzbId;
            try
            {
                Logger.Log("Checking Queue for: [{0}] or [{1}]", rssTitle, rssTitleFix);

                string queueRssUrl = String.Format(Config.SabRequest, "mode=queue&output=xml");

                var queueRssReader = new XmlTextReader(queueRssUrl);
                var queueRssDoc = new XmlDocument();
                queueRssDoc.Load(queueRssReader);

                XmlNodeList queue = queueRssDoc.GetElementsByTagName(@"queue");
                XmlNodeList error = queueRssDoc.GetElementsByTagName(@"error");
                if (error.Count != 0)
                {
                    Logger.Log("Sab Queue Error: {0}", true, error[0].InnerText);
                }

                else if (queue.Count != 0)
                {
                    XmlNodeList slot = ((XmlElement) queue[0]).GetElementsByTagName("slot");

                    foreach (object s in slot)
                    {
                        var queueElement = (XmlElement) s;

                        //Queue is empty
                        if (String.IsNullOrEmpty(queueElement.InnerText))
                            return false;

                        string fileName = queueElement.GetElementsByTagName("filename")[0].InnerText.ToLower();

                        if (Config.VerboseLogging)
                            Logger.Log("Checking Queue Item for match: " + fileName);

                        if (fileName.ToLower() == CleanString(rssTitle).ToLower() ||
                            fileName.ToLower() == CleanString(rssTitleFix).ToLower() ||
                                fileName.ToLower().Contains(nzbId))
                        {
                            Logger.Log("Episode in queue - '{0}'", true, rssTitle);
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log("An Error has occurred while checking the queue. {0}", true, ex);
            }

            return false;
        }

        // TODO: fix this?

        //public bool IsInHistory(string rssTitle, string rssTitleFix)
        //{
        //    //slot other than container for nzb is an issue (slot for each post download step)
        //    try
        //    {
        //        Logger.Log("Checking History for: [{0}] or [{1}]", rssTitle, rssTitleFix);

        //        string historyRssUrl = String.Format(Config.SabRequest, "mode=history&output=xml&start=1&limit=20");

        //        var historyRssReader = new XmlTextReader(historyRssUrl);
        //        var historyRssDoc = new XmlDocument();
        //        historyRssDoc.Load(historyRssReader);

        //        XmlNodeList history = historyRssDoc.GetElementsByTagName(@"history");
        //        XmlNodeList error = historyRssDoc.GetElementsByTagName(@"error");
        //        if (error.Count != 0)
        //        {
        //            Logger.Log("Sab History Error: {0}", true, error[0].InnerText);
        //        }

        //        else if (history.Count != 0)
        //        {
        //            XmlNodeList slot = ((XmlElement)history[0]).GetElementsByTagName("slot");

        //            foreach (object s in slot)
        //            {
        //                var queueElement = (XmlElement)s;

        //                //Queue is empty
        //                if (String.IsNullOrEmpty(queueElement.InnerText))
        //                    return false;

        //                string name = queueElement.GetElementsByTagName("name")[0].InnerText.ToLower();
        //                string nzbName = queueElement.GetElementsByTagName("nzb_name")[0].InnerText.ToLower().Replace(".nzb", "").Replace('.', ' ').Replace('-', ' ');

        //                if (Config.VerboseLogging)
        //                    Logger.Log("Checking History Item for match: " + name);

        //                if (name.ToLower() == CleanString(rssTitle).ToLower() ||
        //                    nzbName.ToLower() == CleanString(rssTitleFix).ToLower())
        //                {
        //                    Logger.Log("Episode in history - '{0}'", true, rssTitle);
        //                    return true;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Log("An Error has occurred while checking the history. {0}", true, ex);
        //    }

        //    return false;
        //}

        #endregion

        private static string SendRequest(string request)
        {
            Logger.Log("DEBUG: " + request);

            var webClient = new WebClient();
            string response = webClient.DownloadString(request).Replace("\n", string.Empty);

            Logger.Log("Queue Response: [{0}]", response);
            return response;
        }

        private string CleanUrlString(string name)
        {
            string result = name;
            string[] badCharacters =
                {
                    "%", "<", ">", "#", "{", "}", "|", "\\", "^", "`", "[", "]", "`", ";", "/", "?",
                    ":", "@", "=", "&", "$"
                };
            string[] goodCharacters =
                {
                    "%25", "%3C", "%3E", "%23", "%7B", "%7D", "%7C", "%5C", "%5E", "%7E", "%5B",
                    "%5D", "%60", "%3B", "%2F", "%3F", "%3A", "%40", "%3D", "%26", "%24"
                };

            for (int i = 0; i < badCharacters.Length; i++)
                result = result.Replace(badCharacters[i], Config.SabReplaceChars ? goodCharacters[i] : "");

            return result.Trim();
        }

        private string CleanString(string name)
        {
            string result = name;
            string[] badCharacters = {"\\", "/", "<", ">", "?", "*", ":", "|", "\""};
            string[] goodCharacters = {"+", "+", "{", "}", "!", "@", "-", "#", "`"};

            for (int i = 0; i < badCharacters.Length; i++)
                result = result.Replace(badCharacters[i], Config.SabReplaceChars ? goodCharacters[i] : "");

            return result.Trim();
        }
    }
}