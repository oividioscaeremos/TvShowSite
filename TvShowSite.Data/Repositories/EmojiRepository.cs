using TvShowSite.Data.Common;
using TvShowSite.Data.Common.Connections;
using TvShowSite.Domain.Entities.EmojiEntities;
using TvShowSite.Domain.TableEntities;

namespace TvShowSite.Data.Repositories
{
    public class EmojiRepository : BaseRepository<Emoji>
    {
        public EmojiRepository(SiteDbConnection connection) : base(connection)
        {

        }

        public async Task<IEnumerable<Emoji>> GetEmojisAsync(bool isComment)
        {
            return await QueryAsync(@"
                SELECT * FROM site.Emoji
                WHERE IsShow = @IsShow
                AND IsComment = @IsComment
                AND IsDeleted <> TRUE
                ORDER BY Id
            ", new Dictionary<string, object>()
            {
                { "IsShow", !isComment },
                { "IsComment", isComment }
            });
        }
    }
}
