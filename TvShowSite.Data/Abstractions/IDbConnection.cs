using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TvShowSite.Data.Abstractions
{
    public class DbConnection
    {
        public IDbConnection connection { 
            get
            {
                return new SqlConnection("");
            }
        }
    }
}
