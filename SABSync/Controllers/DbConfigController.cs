using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using log4net;
using SABSync.Services;
using System.Windows.Forms;
using SubSonic.Repository;

namespace SABSync.Controllers
{
    public class DbConfigController : IConfigController
    {
        private readonly ILog _logger;
        private readonly IDiskController _diskController;
        private readonly IRepository _sonicRepo;



        public DbConfigController(ILog logger, IDiskController diskController, IRepository dataRepository)
        {
            _logger = logger;
            _diskController = diskController;
            _sonicRepo = dataRepository;
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

            var dbValue = _sonicRepo.Single<Repository.Config>(key);

            if (dbValue !=null && !String.IsNullOrWhiteSpace(dbValue.Value))
            {
                return dbValue.Value;
            }


            _logger.WarnFormat("Unable to find config key '{0}' defaultValue:'{1}'", key, defaultValue);
            if (makePermanent)
            {
                SetValue(key, defaultValue.ToString());
            }
            value = defaultValue.ToString();


            return value;
        }

        private void SetValue(string key, string value)
        {
            _logger.DebugFormat("Writing Setting to file. Key:'{0}' Value:'{1}'", key, value);

            _sonicRepo.Add(new Repository.Config() { Key = key, Value = value });
        }

    }
}
