using TvShowSite.Data.Common;
using TvShowSite.Data.Common.Connections;
using TvShowSite.Domain.TableEntities.BagEntities;

namespace TvShowSite.Data.Repositories.BagRepositories
{
    public class EpisodeEmojiRepository : BaseBagRepository<EpisodeEmoji>
    {
        public EpisodeEmojiRepository(SiteDbConnection connection) : base(connection)
        {

        }

    }
}
