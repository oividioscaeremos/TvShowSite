using TvShowSite.Core.Helpers;
using TvShowSite.Data.Common;
using TvShowSite.Data.Common.Connections;
using TvShowSite.Domain.Entities.ShowEntities;
using TvShowSite.Domain.TableEntities.BagEntities;

namespace TvShowSite.Data.Repositories.BagRepositories
{
    public class UserEpisodeRepository : BaseBagRepository<UserEpisode>
    {
        public UserEpisodeRepository(SiteDbConnection connection) : base(connection)
        {

        }

        public async Task<IEnumerable<UserShowHomeEntity>> GetUserNextToWatchAsync(int userId, int? showId = null)
        {
            return await QueryAsync<UserShowHomeEntity>($@"
                SELECT 
                    E.ShowId,
                    MIN(E.Id) as EpisodeId,
                    (SELECT SeasonNumber FROM site.Season WHERE Id = MIN(S.Id)),
                    MIN(S.Id) as SeasonId,
                    (SELECT EpisodeNumber FROM site.Episode WHERE Id = MIN(E.Id)),
                    (SELECT CONCAT(@PosterBase, PosterURL) as PosterURL FROM site.Show WHERE Id = MIN(E.ShowId)),
                    (SELECT Name FROM site.Show WHERE Id = MIN(E.ShowId))
                FROM
                    site.Episode E
                    LEFT OUTER JOIN site.UserEpisode UE
                    ON UE.EpisodeId = E.Id
                    AND UE.IsDeleted <> TRUE
                    JOIN site.Season S
                    ON E.SeasonId = S.Id
                WHERE E.ShowId IN (
                    SELECT ShowId FROM site.UserShow
                    WHERE UserId = @UserId
                    AND IsDeleted <> TRUE
                )
                AND E.Id NOT IN (
                    SELECT UEE.EpisodeId FROM site.UserEpisode UEE
                    WHERE UEE.UserId = @UserId
                    AND IsDeleted <> TRUE
                )
                {(showId.HasValue ? "AND E.ShowId = @ShowId" : "")}
                GROUP BY E.ShowId
            ", new Dictionary<string, object>()
            {
                { "UserId", userId },
                { "PosterBase", SettingsHelper.Settings?.ApiDetails?.TheMovieDbOrg?.ImageBaseUrl ?? "" },
                { "ShowId", showId ?? -1 }
            });
        }

        public async Task<IEnumerable<int>> GetUserLastWatchedShowIdsAsync(int userId)
        {
            return await QueryAsync<int>(@"
                SELECT
                    EP.ShowId
                FROM 
                    site.UserEpisode E,
                    site.Episode EP
                WHERE
                    E.EpisodeId = EP.Id
                    AND E.UserId = @UserId
                GROUP BY EP.ShowId
                ORDER BY MAX(E.InsertDate) DESC
            ", new Dictionary<string, object>()
            {
                { "UserId", userId }
            });
        }

        public async Task MarkAsDeletedByUserIdAndEpisodeIdAsync(int episodeId, int userId)
        {
            await QueryAsync(@"
                UPDATE site.UserEpisode
                SET IsDeleted = TRUE,
                UpdateDate = NOW(),
                UpdatedBy = @UserId
                WHERE UserId = @UserId
                AND EpisodeId = @EpisodeId
                AND IsDeleted <> TRUE
            ", new Dictionary<string, object>()
            {
                { "UserId", userId },
                { "EpisodeId", episodeId }
            });
        }
    }
}
