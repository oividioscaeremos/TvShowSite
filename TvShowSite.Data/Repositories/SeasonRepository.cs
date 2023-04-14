using TvShowSite.Data.Common;
using TvShowSite.Data.Common.Connections;
using TvShowSite.Domain.TableEntities;

namespace TvShowSite.Data.Repositories
{
    public class SeasonRepository : BaseRepository<Season>
    {
        public SeasonRepository(SiteDbConnection connection) : base(connection)
        {

        }

    }
}
