using TvShowSite.Data.Common;
using TvShowSite.Data.Common.Connections;
using TvShowSite.Domain.Entities.EmojiEntities;
using TvShowSite.Domain.TableEntities.BagEntities;

namespace TvShowSite.Data.Repositories.BagRepositories
{
    public class EpisodeEmojiRepository : BaseBagRepository<EpisodeEmoji>
    {
        public EpisodeEmojiRepository(SiteDbConnection connection) : base(connection)
        {

        }

        public async Task<IEnumerable<GetEpisodeReactionsResponseEntity>> GetEpisodeReactionsAsync(int episodeId, int userId)
        {
            return await QueryAsync<GetEpisodeReactionsResponseEntity>(@"
				SELECT * FROM (
	                SELECT
		                E.Id as EmojiId,
		                COUNT(*) as ReactionCount,
		                CASE
			                WHEN (SELECT COUNT(*) FROM site.EpisodeEmoji EE2 WHERE EE2.EpisodeId = @EpisodeId AND EE2.UserId = @UserId AND EE2.IsDeleted <> TRUE) > 0 THEN 1
			                ELSE 0
		                END as IsUsersReaction
	                FROM
		                site.Emoji E,
		                site.EpisodeEmoji EE
	                WHERE E.Id = EE.EmojiId
	                AND EE.EpisodeId = @EpisodeId
	                AND E.IsDeleted <> TRUE
	                AND EE.IsDeleted <> TRUE
	                GROUP BY E.Id
                ) X
                ORDER BY X.IsUsersReaction DESC, X.ReactionCount DESC
			", new Dictionary<string, object>()
			{
				{ "EpisodeId", episodeId },
				{ "UserId", userId }
			});
        }

        public async Task MarkEpisodeRelatedReactionsAsDeletedByUserIdAsync(int episodeId, int userId)
        {
            await QueryAsync(@"
                UPDATE site.EpisodeEmoji
                SET IsDeleted = TRUE,
                UpdateDate = NOW(),
                UpdatedBy = @UserId
                WHERE EpisodeId = @EpisodeId
                AND UserId = @UserId
                AND IsDeleted <> TRUE
            ", new Dictionary<string, object>()
            {
                { "EpisodeId", episodeId },
                { "UserId", userId }
            });
        }
    }
}
