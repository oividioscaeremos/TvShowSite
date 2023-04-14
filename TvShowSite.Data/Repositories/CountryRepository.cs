using TvShowSite.Data.Common;
using TvShowSite.Data.Common.Connections;
using TvShowSite.Domain.TableEntities;

namespace TvShowSite.Data.Repositories
{
    public class CountryRepository : BaseRepository<Country>
    {
        public CountryRepository(SiteDbConnection connection) : base(connection)
        {

        }

    }
}
