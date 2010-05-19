using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;

namespace PreQueue
{
    class Config
    {
        private static readonly NameValueCollection Settings = ConfigurationManager.AppSettings;

        static Config()
        {
            ConsoleCategoryMaps = GetCategoryMap();
            ShowAliases = GetShowAliases();
        }

        public static IList<ConsoleCategoryMap> ConsoleCategoryMaps { get; set; }
        public static IList<ShowAlias> ShowAliases { get; set; }

        private static IList<ConsoleCategoryMap> GetCategoryMap()
        {
            var categoryConfigFile = new FileInfo(Settings["consoleCategories"]);
            if (!categoryConfigFile.Exists)
                throw new ApplicationException("Invalid Category file path. " + categoryConfigFile);

            var list = new List<ConsoleCategoryMap>();
            int line = 0;
            foreach (string categoryLine in File.ReadAllLines(categoryConfigFile.FullName))
            {
                line++;
                string[] parts = categoryLine.Split('|');
                if (parts.Length != 2)
                    throw new ApplicationException(string.Format("Invalid Console Category configuration at line {0}.", line));

                list.Add(new ConsoleCategoryMap { ConsoleName = parts[0], SabCategory = parts[1] });
            }
            return list;
        }

        private static IList<ShowAlias> GetShowAliases()
        {
            var aliasConfigFile = new FileInfo(Settings["alias"]);
            if (!aliasConfigFile.Exists)
                throw new ApplicationException("Invalid Alias file path. " + aliasConfigFile);

            var list = new List<ShowAlias>();
            int line = 0;
            foreach (string aliasLine in File.ReadAllLines(aliasConfigFile.FullName))
            {
                line++;
                string[] parts = aliasLine.Split('|');
                if (parts.Length != 2)
                    throw new ApplicationException(string.Format("Invalid Alias configuration at line {0}.", line));

                list.Add(new ShowAlias { BadName = parts[0], Alias = parts[1] });
            }
            return list;
        }
    }
}
