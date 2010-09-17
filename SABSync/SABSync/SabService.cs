// TODO: need to change Log statements that were originally meant to be in Summary report

using System;
using System.Linq;
using System.Net;
using System.Xml;
using System.Xml.Linq;

namespace SABSync
{
    public interface ISabService
    {
        string AddByUrl(NzbInfo nzb);
        bool IsInQueue(Episode episode);
        bool IsInHistory(Episode episode);
    }

    public interface ISabRequest
    {
        XDocument GetHistory();
    }

    public class SabRequest : ISabRequest
    {
        public SabRequest()
        {
            Config = new Config();
        }

        private Config Config { get; set; }

        #region ISabRequest Members

        public XDocument GetHistory()
        {
            string uri = string.Format(Config.SabRequest, "mode=history&output=xml&start=0&limit=100");
            return XDocument.Load(uri);
        }

        #endregion
    }

    public class SabService : ISabService
    {
        private static readonly Logger Logger = new Logger();

        public SabService() : this(new Config(), new SabRequest())
        {
        }

        public SabService(Config config, ISabRequest sabRequest)
        {
            Config = config;
            SabRequest = sabRequest;
        }

        private Config Config { get; set; }
        private ISabRequest SabRequest { get; set; }

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

        public bool IsInQueue(Episode episode)
        {
            string rssTitle = episode.FeedItem.Title;
            string rssTitleFix = episode.FeedItem.TitleFix;
            string nzbId = episode.FeedItem.NzbId;
            try
            {
                Logger.Log("Checking Queue for  : [{0}] or [{1}]", rssTitle, rssTitleFix);

                string queueRssUrl = String.Format(Config.SabRequest, "mode=queue&output=xml");

                var queueRssReader = new XmlTextReader(queueRssUrl);
                var queueRssDoc = new XmlDocument();
                queueRssDoc.Load(queueRssReader);

                XmlNodeList queue = queueRssDoc.GetElementsByTagName(@"queue");
                XmlNodeList error = queueRssDoc.GetElementsByTagName(@"error");
                if (error.Count != 0)
                {
                    //Logger.Log("Sab Queue Error: {0}", true, error[0].InnerText);
                    Logger.Log("Sab Queue Error: {0}", error[0].InnerText);
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
                            //Logger.Log("Episode in queue - '{0}'", true, rssTitle);
                            Logger.Log("Episode in queue - '{0}'", rssTitle);
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Logger.Log("An Error has occurred while checking the queue. {0}", true, ex);
                Logger.Log("An Error has occurred while checking the queue. {0}", ex);
            }

            return false;
        }

        public bool IsInHistory(Episode episode)
        {
            Logger.Log("Checking History for: [{0}]", episode.Title);
            try
            {
                XDocument xml = SabRequest.GetHistory();
                string error = GetError(xml);
                if (error != null)
                {
                    //Logger.Log("Sab History Error: {0}", true, error);
                    Logger.Log("Sab History Error: {0}", error);
                    return false;
                }

                bool found = xml.Elements("history").Elements("slots").Elements("slot")
                    .Select(name => (string) name.Element("name"))
                    .Any(name => name.StartsWith(episode.Title, StringComparison.InvariantCultureIgnoreCase));
                if (found)
                {
                    //Logger.Log("Episode in history - '{0}'", true, episode.Title);
                    Logger.Log("Episode in history - '{0}'", episode.Title);
                    return true;
                }
            }
            catch (Exception ex)
            {
                //Logger.Log("An Error has occurred while checking the history. {0}", true, ex);
                Logger.Log("An Error has occurred while checking the history. {0}", ex);
            }
            return false;
        }

        #endregion

        private static string GetError(XDocument xml)
        {
            return (from element in xml.Elements("result")
                    select (string) element.Element("error")).FirstOrDefault();
        }

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