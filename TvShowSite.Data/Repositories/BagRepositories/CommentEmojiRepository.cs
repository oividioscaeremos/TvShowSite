using TvShowSite.Data.Common;
using TvShowSite.Data.Common.Connections;
using TvShowSite.Domain.Entities.EmojiEntities;
using TvShowSite.Domain.TableEntities.BagEntities;

namespace TvShowSite.Data.Repositories.BagRepositories
{
    public class CommentEmojiRepository : BaseBagRepository<CommentEmoji>
    {
        public CommentEmojiRepository(SiteDbConnection connection) : base(connection)
        {

        }

        public async Task<IEnumerable<GetCommentReactionsResponseEntity>> GetCommentReactionsAsync(int commentId, int userId)
        {
            return await QueryAsync<GetCommentReactionsResponseEntity>(@"
                SELECT * FROM (
	                SELECT
		                E.Id as EmojiId,
                        MAX(E.EmojiClassName) as EmojiClassName,
		                COUNT(*) as ReactionCount,
		                CASE
			                WHEN (SELECT COUNT(*) FROM site.CommentEmoji CE2 WHERE CE2.CommentId = @CommentId AND CE2.UserId = @UserId AND CE2.IsDeleted <> TRUE) > 0 THEN 1
			                ELSE 0
		                END as IsUsersReaction
	                FROM
		                site.Emoji E,
		                site.CommentEmoji CE
	                WHERE E.Id = CE.EmojiId
	                AND CE.CommentId = @CommentId
	                AND E.IsDeleted <> TRUE
	                AND CE.IsDeleted <> TRUE
	                GROUP BY E.Id
                ) X
                ORDER BY X.IsUsersReaction DESC, X.ReactionCount DESC
            ", new Dictionary<string, object>()
            {
                { "CommentId", commentId },
                { "UserId", userId }
            });
        }

        public async Task MarkCommentRelatedReactionsAsDeletedByUserIdAsync(int commentId, int userId)
        {
            await QueryAsync(@"
                UPDATE site.CommentEmoji
                SET IsDeleted = TRUE,
                UpdateDate = NOW(),
                UpdatedBy = @UserId
                WHERE CommentId = @CommentId
                AND UserId = @UserId
                AND IsDeleted <> TRUE
            ", new Dictionary<string, object>()
            {
                { "CommentId", commentId },
                { "UserId", userId }
            });
        }
    }
}
