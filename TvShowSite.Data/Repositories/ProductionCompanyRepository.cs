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
