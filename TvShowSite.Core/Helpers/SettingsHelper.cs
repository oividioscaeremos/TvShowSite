using Microsoft.Extensions.Configuration;
using TvShowSite.Domain.System;

namespace TvShowSite.Core.Helpers
{
    public static class SettingsHelper
    {
        public static Settings? Settings { get; set; }

        static SettingsHelper()
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{environmentName}.json", true, true)
                .Build();

            Settings = config.Get<Settings>();
        }
    }
}
