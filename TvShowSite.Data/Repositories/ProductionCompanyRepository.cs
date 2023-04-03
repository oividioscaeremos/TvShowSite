using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShowSite.Core.Abstractions.DataAbstractions.Common;
using TvShowSite.Data.Common;
using TvShowSite.Data.Common.Connections;
using TvShowSite.Domain.TableEntities;

namespace TvShowSite.Data.Repositories
{
    public class ProductionCompanyRepository : BaseRepository<ProductionCompany>
    {
        public ProductionCompanyRepository(SiteDbConnection connection) : base(connection)
        {

        }

    }
}
