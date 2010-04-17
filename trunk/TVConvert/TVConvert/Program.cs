﻿using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace TVConvert
{
    class Program
    {
        private static string _logDir = ConfigurationManager.AppSettings["logDir"].ToString(); //Log Directory from app.config
        private static string _logFile = _logDir + @"\TVConvert.txt";
        private static string _tempDir = ConfigurationManager.AppSettings["tempDir"].ToString(); // Temp Directory from app.cpnfig
        private static string _outputDir = ConfigurationManager.AppSettings["outputDir"].ToString(); //Output Directory from app.config
        private static string _handBrakeLocation = ConfigurationManager.AppSettings["handBrakeLocation"].ToString(); //HandBrake location from app.config
        private static string _handBrakePreset = ConfigurationManager.AppSettings["handBrakePreset"].ToString(); //HandBrake location from app.config
        private static string _atomicParsleyLocation = ConfigurationManager.AppSettings["atomicParsleyLocation"].ToString(); //Atomic Parsley from app.config
        private static string _episodeNameFormat = ConfigurationManager.AppSettings["episodeNameFormat"].ToString(); //Which Episode Naming Convention to Use (Poor multi-season sorting by Apple)
        private static string _videoExt = ConfigurationManager.AppSettings["videoExt"].ToString(); //Which Episode Naming Convention to Use (Poor multi-season sorting by Apple)

        static void Main(string[] args)
        {
            string[] videoExt = _videoExt.Split(';');
            foreach (string e in videoExt)
            {
                string fileExtSearch = "*" + e;
                string[] filesToConvert = Directory.GetFiles(_tempDir, fileExtSearch); //Get all files from tempDir
                foreach (string fileToConvert in filesToConvert) //Foreach file found, run HandBrake + Atomic Parsley
                {

                    string fileNameToConvert = Path.GetFileNameWithoutExtension(fileToConvert);

                    string[] fileNameSplit = fileNameToConvert.Split('-'); //Break apart File Name
                    string showName = null;
                    int seasonNumber = 0;
                    int episodeNumber = 0;
                    string episodeName = null;

                    if (fileNameSplit.Length == 3)
                    {
                        showName = fileNameSplit[0].Trim();
                        string seasonEpisode = fileNameSplit[1].Trim();
                        episodeName = fileNameSplit[2].Trim();

                        if (seasonEpisode.Contains("x"))
                        {
                            string[] seasonEpisodeSplit = seasonEpisode.Split('x');
                            Int32.TryParse(seasonEpisodeSplit[0], out seasonNumber);
                            Int32.TryParse(seasonEpisodeSplit[1], out episodeNumber);
                        }

                        else if (seasonEpisode.Contains("S") && seasonEpisode.Contains("E"))
                        {
                            string[] seasonEpisodeSplit = seasonEpisode.Split('E');
                            seasonEpisodeSplit[0].Replace("S", "");
                            Int32.TryParse(seasonEpisodeSplit[0], out seasonNumber);
                            Int32.TryParse(seasonEpisodeSplit[1], out seasonNumber);
                        }

                        string outputFile = RunHandbrake(fileToConvert, fileNameToConvert);
                        File.Delete(fileToConvert);
                        RunAtomicParsley(showName, seasonNumber, episodeNumber, episodeName, outputFile);
                    }

                    if (fileNameSplit.Length == 4)
                    {
                        if (Regex.IsMatch(fileNameSplit[1], @"\d{1,2}x\d{1,2}") || Regex.IsMatch(fileNameSplit[1], @"S\d{1,2}E\d{1,2}"))
                        {
                            showName = fileNameSplit[0].Trim();
                            string seasonEpisode = fileNameSplit[1];
                            episodeName = fileNameSplit[2] + fileNameSplit[3];
                            episodeName.Trim();

                            if (seasonEpisode.Contains("x"))
                            {
                                string[] seasonEpisodeSplit = seasonEpisode.Split('x');
                                Int32.TryParse(seasonEpisodeSplit[0], out seasonNumber);
                                Int32.TryParse(seasonEpisodeSplit[1], out episodeNumber);
                            }

                            else if (seasonEpisode.Contains("S") && seasonEpisode.Contains("E"))
                            {
                                string[] seasonEpisodeSplit = seasonEpisode.Split('E');
                                seasonEpisodeSplit[0].Replace("S", "");
                                Int32.TryParse(seasonEpisodeSplit[0], out seasonNumber);
                                Int32.TryParse(seasonEpisodeSplit[1], out seasonNumber);
                            }

                            string outputFile = RunHandbrake(fileToConvert, fileNameToConvert);
                            File.Delete(fileToConvert);
                            RunAtomicParsley(showName, seasonNumber, episodeNumber, episodeName, outputFile);
                        }

                        else if (Regex.IsMatch(fileNameSplit[2], @"\d{1,2}x\d{1,2}") || Regex.IsMatch(fileNameSplit[2], @"S\d{1,2}E\d{1,2}"))
                        {
                            showName = fileNameSplit[0] + fileNameSplit[1];
                            showName.Trim();
                            string seasonEpisode = fileNameSplit[2];
                            episodeName = fileNameSplit[3].Trim();

                            if (seasonEpisode.Contains("x"))
                            {
                                string[] seasonEpisodeSplit = seasonEpisode.Split('x');
                                Int32.TryParse(seasonEpisodeSplit[0], out seasonNumber);
                                Int32.TryParse(seasonEpisodeSplit[1], out episodeNumber);
                            }

                            else if (seasonEpisode.Contains("S") && seasonEpisode.Contains("E"))
                            {
                                string[] seasonEpisodeSplit = seasonEpisode.Split('E');
                                seasonEpisodeSplit[0].Replace("S", "");
                                Int32.TryParse(seasonEpisodeSplit[0], out seasonNumber);
                                Int32.TryParse(seasonEpisodeSplit[1], out seasonNumber);
                            }

                            string outputFile = RunHandbrake(fileToConvert, fileNameToConvert);
                            File.Delete(fileToConvert);
                            RunAtomicParsley(showName, seasonNumber, episodeNumber, episodeName, outputFile);
                        }

                        else
                        {
                            //Run HandBrake, no Atomic Parsley
                            Console.WriteLine("Unsupported Format");
                            RunHandbrake(fileToConvert, fileNameToConvert);
                            File.Delete(fileToConvert);
                        }
                    }

                    else
                    {
                        //Run HandBrake, no Atomic Parsley
                        Console.WriteLine("Unsupported Formart");
                        RunHandbrake(fileToConvert, fileNameToConvert);
                        File.Delete(fileToConvert);
                    }
                }
            }
        }

        private static string RunHandbrake(string inputFile, string inputFileName)
        {
            string handBreakReplace = _handBrakePreset.Replace(" AND ", " & ");
            string outputFile = _outputDir + "\\" + inputFileName + ".mp4";
            string handBrakeCommands = "-i \"" + inputFile + "\" -o \"" + outputFile + "\" --preset=\"" + handBreakReplace + "\""; //Commands for Handbrake
            string handBrakeFile = _handBrakeLocation + "\\handbrakeCLI.exe"; //Path to handbrake.exe
            Process.Start(handBrakeFile, handBrakeCommands).WaitForExit(); //Run HandBrake and wait for Exit
            return outputFile;
        }

        private static string RunAtomicParsley(string showName, int seasonNumber, int episodeNumber, string episodeName, string outputFile)
        {
            if (_episodeNameFormat.Contains("none")) //If only episode name should be in episode name field
            {
                string atomicParsleyCommands = "\"" + outputFile + "\" --overWrite --title \"" + episodeName + "\" --genre \"TV Shows\" --stik \"TV Show\" --TVShowName \"" + showName + "\" --TVEpisodeNum \"" + episodeNumber + "\" --TVSeason \"" + seasonNumber + "\""; //Build string for AtomicParsley arguments
                string atomicParsleyFile = _atomicParsleyLocation + "\\AtomicParsley.exe"; //Create string for path + AtomicParsley.exe
                Process.Start(atomicParsleyFile, atomicParsleyCommands).WaitForExit(); //Run AtomicParsley and Wait for Exit
            }

            else if (_episodeNameFormat.Contains("episode")) //If Episode Number + Name should be in Episode Title (Number - Title)
            {
                string atomicParsleyCommands = "\"" + outputFile + "\" --overWrite --title \"" + episodeNumber + " - " + episodeName + "\" --genre \"TV Shows\" --stik \"TV Show\" --TVShowName \"" + showName + "\" --TVEpisodeNum \"" + episodeNumber + "\" --TVSeason \"" + seasonNumber + "\""; //Build string for AtomicParsley arguments
                string atomicParsleyFile = _atomicParsleyLocation + "\\AtomicParsley.exe"; //Create string for path + AtomicParsley.exe
                Process.Start(atomicParsleyFile, atomicParsleyCommands).WaitForExit(); //Run AtomicParsley and Wait for Exit
            }

            else if (_episodeNameFormat.Contains("both")) //If Season/Episode Number + Name should be in Episode Title (SeasonNumber'x'EpisodeNumber - Title)
            {
                string atomicParsleyCommands = "\"" + outputFile + "\" --overWrite --title \"" + seasonNumber + "x" + episodeNumber + " - " + episodeName + "\" --genre \"TV Shows\" --stik \"TV Show\" --TVShowName \"" + showName + "\" --TVEpisodeNum \"" + episodeNumber + "\" --TVSeason \"" + seasonNumber + "\""; //Build string for AtomicParsley arguments
                string atomicParsleyFile = _atomicParsleyLocation + "\\AtomicParsley.exe"; //Create string for path + AtomicParsley.exe
                Process.Start(atomicParsleyFile, atomicParsleyCommands).WaitForExit(); //Run AtomicParsley and Wait for Exit
            }

            else //handle as none (default)
            {
                string atomicParsleyCommands = "\"" + outputFile + "\" --overWrite --title \"" + episodeName + "\" --genre \"TV Shows\" --stik \"TV Show\" --TVShowName \"" + showName + "\" --TVEpisodeNum \"" + episodeNumber + "\" --TVSeason \"" + seasonNumber + "\""; //Build string for AtomicParsley arguments
                string atomicParsleyFile = _atomicParsleyLocation + "\\AtomicParsley.exe"; //Create string for path + AtomicParsley.exe
                Process.Start(atomicParsleyFile, atomicParsleyCommands).WaitForExit(); //Run AtomicParsley and Wait for Exit
            }
            return outputFile;
        }

        private static string GetShowName(string fileName)
        {
            string[] titleSplit = null;
            string[] titleSplit = null;
            string[] titleSplitDaily = null;

            string patternMulti = @"[Ss](?<Season>(?:\d{1,2}))[Ee](?<EpisodeOne>(?:\d{1,2}))E(?<EpisodeTwo>(?:\d{1,2}))";
            string pattern = @"S(?<Season>(?:\d{1,2}))E(?<Episode>(?:\d{1,2}))";
            string patternDaily = @"(?<Year>\d{4}).{1}(?<Month>\d{2}).{1}(?<Day>\d{2})";

            Match titleMatchMulti = Regex.Match(title, patternMulti);

            if (titleMatchMulti.Success)
            {
                return fileName;
            }

            return fileName;
        }

        private static int GetSeasonNumber(string fileName)
        {
            return 0;
        }

        private static int GetEpisodeNumber(string fileName)
        {
            return 0;
        }


        private static string GetEpisodeName(string fileName)
        {
            return fileName;
        }

        private static void Log(string message)
        {
            Console.WriteLine(message);
            try
            {
                using (StreamWriter sw = File.AppendText(_logFile))
                {
                    sw.WriteLine(message);
                }
            }
            catch { }
        }

        private static void Log(string message, params object[] para)
        {

            Log(String.Format(message, para));
        }
    }
}