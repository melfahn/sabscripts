﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using log4net;
using SABSync.Properties;

namespace SABSync.Services
{
    class ConfigController : IConfigController
    {
        private readonly ILog _logger;
        private readonly IDiskController _diskController;

        private readonly Configuration _config =
            ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

        public ConfigController(ILog logger, IDiskController diskController)
        {
            _logger = logger;
            _diskController = diskController;
        }

        public List<String> GetTvRoots()
        {
            return (GetValue("tvRoot").Trim(';').Split(';').Where(path => _diskController.Exists(path))).ToList();
        }


        private string GetValue(string key)
        {
            return GetValue(key, String.Empty, false);
        }

        private string GetValue(string key, object defaultValue, bool makePermanent)
        {
            string value;

            if (_config.AppSettings.Settings[key] != null)
            {
                value = _config.AppSettings.Settings[key].Value;
            }
            else
            {
                _logger.WarnFormat("Unable to find config key '{0}' defaultValue:'{1}'", key, defaultValue);
                if (makePermanent)
                {
                    SetValue(key, defaultValue.ToString());
                }
                value = defaultValue.ToString();
            }

            return value;
        }

        private void SetValue(string key, object value)
        {
            _logger.DebugFormat("Writing Setting to file. Key:'{0}' Value:'{1}'", key, value);
            _config.AppSettings.Settings.Remove(key);
            _config.AppSettings.Settings.Add(key, value.ToString());
            _config.Save();
        }

    }
}
