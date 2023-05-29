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
                SELECT * FROM (
                    SELECT 
                        ROW_NUMBER() OVER(PARTITION BY SH.Id ORDER BY CONCAT(to_char(SE.SeasonNumber, '000'), to_char(EP.EpisodeNumber, '000')) ASC) as ROWNUM, 
                        SH.Id as ShowId,
                        EP.Id as EpisodeId,
                        SE.Id as SeasonId,
                        CONCAT(@PosterBase, SH.PosterURL) as PosterURL,
                        SH.Name,
                        SE.SeasonNumber, 
                        EP.EpisodeNumber
                    FROM 
                        site.Season SE,
                        site.Episode EP,
                        site.Show SH
                    WHERE SH.Id = EP.ShowId 
                    AND EP.SeasonId = SE.Id
                    AND EP.ShowId IN (
                        SELECT ShowId FROM site.UserShow
                        WHERE UserId = @UserId
                        {(showId.HasValue ? "" : "AND IsDeleted <> TRUE")}
                    )
                    AND SE.SeasonNumber > 0
                    AND EP.Id NOT IN (
                        SELECT UEE.EpisodeId FROM site.UserEpisode UEE
                        WHERE UEE.UserId = @UserId
                        AND IsDeleted <> TRUE
                    )
                    {(showId.HasValue ? "AND EP.ShowId = @ShowId" : "")}
                ) X
                WHERE X.ROWNUM = 1
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
