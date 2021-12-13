using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using Tasks;
using Tasks.Application.AsyncServices;

[assembly: OwinStartup(typeof(MyTestApp.Startup))]

namespace MyTestApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseHangfireAspNet(GetHangfireServers);
            app.UseHangfireDashboard();
            HangfireService.RunJobs();
        }

        private static IEnumerable<IDisposable> GetHangfireServers()
        {
            GlobalConfiguration.Configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseUnityActivator(UnityConfig.GetConfiguredContainer())
                .UseSqlServerStorage(ConfigurationManager.ConnectionStrings["Hangfire_SQLServer"].ConnectionString, new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                });

            yield return new BackgroundJobServer();
        }
    }
}
