using System;
using System.Collections.Generic;
using System.Text;

namespace PreQueue
{
    class ProcessConsole
    {
        private static readonly Logger Logger = new Logger();
        internal static string GetNzbName(string nzbName)
        {
            //Process Console
            //Get the type of Console Game (360 (XBLA, DLC, ISO), DS, Wii, etc) - If not found use default "Consoles" Category, store category mapping in config file
            //User can define their own list of strings in nzbName to set the sabCategory to - Matches from top down (should be ordered from most to least strict)

            string sabCategory = null;
            foreach (ConsoleCategoryMap catMap in Config.ConsoleCategoryMaps)
            {
                if (nzbName.ToLower().Contains(catMap.ConsoleName.ToLower()))
                {
                    sabCategory = catMap.SabCategory;
                    return sabCategory;
                }
            }
            return sabCategory;
        }
    }
}
