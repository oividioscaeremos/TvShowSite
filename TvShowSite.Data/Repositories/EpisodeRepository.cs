using TvShowSite.Data.Common;
using TvShowSite.Data.Common.Connections;
using TvShowSite.Domain.TableEntities;

namespace TvShowSite.Data.Repositories
{
    public class EpisodeRepository : BaseRepository<Episode>
    {
        public EpisodeRepository(SiteDbConnection connection) : base(connection)
        {

        }

    }
}
