using System;
using System.Collections.Generic;
using log4net;
using Ninject;
using SABSync.Controllers;
using SABSync.Services;
using SubSonic.DataProviders;
using SubSonic.Repository;

namespace SABSync.ServiceLocator
{
    public static class Services
    {

        public static void BindKernel(IKernel kernel)
        {
            var provider = ProviderFactory.GetProvider("Data Source=filename;Version=3;","System.Data.SQLite");


            kernel.Bind<ISeriesController>().To<SeriesController>();
            kernel.Bind<IDiskController>().To<DiskController>();
            kernel.Bind<ITvDbController>().To<TvDbController>();
            kernel.Bind<IConfigController>().To<DbConfigController>();
            kernel.Bind<ILog>().ToMethod(c => LogManager.GetLogger("logger-name"));
            kernel.Bind<IRepository>().ToMethod(c => new SimpleRepository(provider, SimpleRepositoryOptions.RunMigrations));
        }


    }
}
