using TvShowSite.Data.Common;
using TvShowSite.Data.Common.Connections;
using TvShowSite.Domain.TableEntities.BagEntities;

namespace TvShowSite.Data.Repositories.BagRepositories
{
    public class CommentEmojiRepository : BaseBagRepository<CommentEmoji>
    {
        public CommentEmojiRepository(SiteDbConnection connection) : base(connection)
        {

        }

    }
}
