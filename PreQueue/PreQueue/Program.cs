using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace PreQueue
{
    class Program
    {
        private static readonly Logger Logger = new Logger();

        static void Main(string[] args)
        {
            //Set Current Working Directory to Current Executing Assembly's Directory
            Environment.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;            

            if (args.Length != 11)
            {
                Logger.Log("Invalid Arguments - Quitting");
                return;
            }

            else
            {
                try
                {
                    Logger.Log("Input:");
                    foreach (var a in args)
                    {
                        Logger.Log(a);
                    }

                    //Parameters from SABnzbd
                    //1 : Name of the NZB (no path, no ".nzb")
                    //2 : PP (0, 1, 2 or 3)
                    //3 : Category
                    //4 : Script (no path)
                    //5 : Priority (-100, -1, 0 or 1 meaning Default, Low, Normal, High)
                    //6 : Size of the download (in bytes)
                    //7 : Group list (separated by spaces)
                    //8 : Show name
                    //9 : Season (1..99)
                    //10 : Episode (1..99)
                    //11: Episode name

                    //Array starts at 0
                    string nzbNameSab = args[0];
                    int postProc = 3;
                    Int32.TryParse(args[1], out postProc);
                    string category = args[2];
                    string script = args[3];
                    int prioritySab = -100;
                    Int32.TryParse(args[4], out prioritySab);
                    long size = 0;
                    long.TryParse(args[5], out size);
                    string[] groupList = args[6].Split(' ');
                    string showNameSab = args[7];
                    int seasonNumberSab = 0;
                    Int32.TryParse(args[8], out seasonNumberSab);
                    int episodeNumberSab = 0;
                    Int32.TryParse(args[9], out episodeNumberSab);
                    string episodeNameSab = args[10];

                    string nzbName = nzbNameSab;

                    //TV Category
                    if (category.ToLower() == "tv")
                    {
                        if (episodeNameSab != "")
                            nzbName = ProcessTv.GetNzbName(showNameSab, seasonNumberSab, episodeNumberSab, episodeNameSab);

                        else if (showNameSab != "" && episodeNameSab == "")
                            nzbName = ProcessTv.GetNzbName(showNameSab, seasonNumberSab, episodeNumberSab);

                        else
                            nzbName = ProcessTv.GetNzbName(nzbName);

                        SendToSab(nzbName, postProc, category, script, prioritySab, "");
                        return;
                    }

                    //Movie Category
                    else if (category.ToLower() == "movies")
                    {
                        nzbName = ProcessMovie.GetNzbName(nzbNameSab);
                        SendToSab(nzbName, postProc, category, script, prioritySab, "");
                        return;
                    }

                    //Console Category
                    else if (category.ToLower() == "consoles" || category.ToLower() == "games")
                    {
                        category = ProcessConsole.GetNzbName(nzbNameSab);
                        SendToSab(nzbName, postProc, category, script, prioritySab, null);
                        return;
                    }

                    //No Category, but SABnzbd Detected a TV Show
                    else if (showNameSab != "" && seasonNumberSab != 0 && episodeNumberSab != 0)
                    {
                        if (episodeNameSab != "")
                            nzbName = ProcessTv.GetNzbName(showNameSab, seasonNumberSab, episodeNumberSab, episodeNameSab);

                        else
                            nzbName = ProcessTv.GetNzbName(showNameSab, seasonNumberSab, episodeNumberSab);

                        category = "tv";

                        SendToSab(nzbName, postProc, category, script, prioritySab, null);
                        return;
                    }

                    //Category found, but is not 'consoles', 'movies' or 'tv'
                    else if (category != "")
                    {
                        SendToSab(nzbName, postProc, category, script, prioritySab, null);
                        return;
                    }

                    //No Category found, check for TV Show, Movie or Console Categories
                    else
                    {
                        //Process unknown Category
                        nzbName = ProcessTv.GetNzbName(nzbNameSab);
                        if (nzbName != nzbNameSab)
                        {
                            category = "tv";
                            SendToSab(nzbName, postProc, category, script, prioritySab, null);
                            return;
                        }

                        category = ProcessConsole.GetNzbName(nzbNameSab);
                        if (category != null)
                        {
                            SendToSab(nzbName, postProc, category, script, prioritySab, null);
                            return;
                        }

                        nzbName = ProcessMovie.GetNzbName(nzbNameSab);
                        if (nzbName != nzbNameSab)
                        {
                            category = "movies";
                            SendToSab(nzbName, postProc, category, script, prioritySab, null);
                            return;
                        }
                    }
                    //If no changes were made, send to SAB for downloading with no Category defined
                    SendToSab(nzbName, postProc, category, script, prioritySab, null);
                }

                catch (Exception ex)
                {
                    Logger.Log(ex.ToString());
                }
            }
        }

        private static void SendToSab(string nzbName, int postProc, string category, string script, int priority, string group)
        {
            //1 : 0=Refuse, 1=Accept, 2
            //2 : Name of the NZB (no path, no ".nzb")
            //3 : PP (0, 1, 2 or 3)
            //4 : Category
            //5 : Script (basename)
            //6 : Priority (-100, -1, 0 or 1)
            //7 : Group to be used (in case your provider doesn't carry all groups and there are multiple groups in the NZB)

            Logger.Log("Output:");
            Logger.Log("1");
            Logger.Log(nzbName);
            Logger.Log(postProc.ToString());
            Logger.Log(category);
            Logger.Log(script);
            Logger.Log(priority.ToString());
            Logger.Log(group);
            Logger.Log("----------------------------------------------------");

            Console.WriteLine("1");
            Console.WriteLine(nzbName);
            Console.WriteLine(postProc.ToString());
            Console.WriteLine(category);
            Console.WriteLine(script);
            Console.WriteLine(priority.ToString());
            Console.WriteLine(group);


        }
    }
}
