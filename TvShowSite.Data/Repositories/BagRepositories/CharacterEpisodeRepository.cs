using TvShowSite.Data.Common;
using TvShowSite.Data.Common.Connections;
using TvShowSite.Domain.TableEntities.BagEntities;

namespace TvShowSite.Data.Repositories.BagRepositories
{
    public class CharacterEpisodeRepository : BaseBagRepository<CharacterEpisode>
    {
        public CharacterEpisodeRepository(SiteDbConnection connection) : base(connection)
        {

        }

    }
}
