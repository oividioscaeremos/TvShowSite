using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TvShowSite.Domain.System
{
    public class Settings
    {
        public ConnectionStrings? ConnectionStrings { get; set; }
    }

    public class ConnectionStrings
    {
        public string? PostgreSql { get; set; }
    }
}
