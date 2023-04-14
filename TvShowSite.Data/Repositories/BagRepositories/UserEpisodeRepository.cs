using TvShowSite.Data.Common;
using TvShowSite.Data.Common.Connections;
using TvShowSite.Domain.TableEntities.BagEntities;

namespace TvShowSite.Data.Repositories.BagRepositories
{
    public class UserEpisodeRepository : BaseBagRepository<UserEpisode>
    {
        public UserEpisodeRepository(SiteDbConnection connection) : base(connection)
        {

        }

    }
}
