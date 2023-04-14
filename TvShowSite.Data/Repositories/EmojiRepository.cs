using TvShowSite.Data.Common;
using TvShowSite.Data.Common.Connections;
using TvShowSite.Domain.TableEntities;

namespace TvShowSite.Data.Repositories
{
    public class EmojiRepository : BaseRepository<Emoji>
    {
        public EmojiRepository(SiteDbConnection connection) : base(connection)
        {

        }

    }
}
