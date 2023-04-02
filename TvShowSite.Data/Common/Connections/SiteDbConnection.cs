using Microsoft.Extensions.Options;
using Npgsql;
using System.Data;
using System.Data.SqlClient;
using TvShowSite.Core.Abstractions.DataAbstractions.Common;
using TvShowSite.Core.Helpers;
using TvShowSite.Domain.System;

namespace TvShowSite.Data.Common.Connections
{
    public class SiteDbConnection : ICustomDbConnection
    {
        private readonly IOptions<UserSecrets> _userSecrets;
        public SiteDbConnection(IOptions<UserSecrets> userSecrets)
        {
            _userSecrets = userSecrets;
        }
        public IDbConnection Connection
        {
            get
            {

                if (SettingsHelper.Settings?.ConnectionStrings?.SiteDbConnectionString is not null)
                {
                    var connection = new NpgsqlConnection(SettingsHelper.Settings.ConnectionStrings.SiteDbConnectionString);

                    connection.Open();

                    return connection;
                }
                else
                {
                    throw new NotImplementedException("Connection string was not found for SiteDb");
                }
            }
        }
    }
}
