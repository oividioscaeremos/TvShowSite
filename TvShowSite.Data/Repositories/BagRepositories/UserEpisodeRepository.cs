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

        public async Task<IEnumerable<UserShowHomeEntity>> GetUserNextToWatchAsync(int userId)
        {
            return await QueryAsync<UserShowHomeEntity>(@"
                SELECT 
                    E.ShowId,
                    MIN(E.Id) as EpisodeId,
                    (SELECT SeasonNumber FROM site.Season WHERE Id = MIN(S.Id)),
                    (SELECT EpisodeNumber FROM site.Episode WHERE Id = MIN(E.Id)),
                    (SELECT CONCAT(@PosterBase, PosterURL) as PosterURL FROM site.Show WHERE Id = MIN(E.ShowId)),
                    (SELECT Name FROM site.Show WHERE Id = MIN(E.ShowId))
                FROM
                    site.Episode E
                    LEFT OUTER JOIN site.UserEpisode UE
                    ON UE.EpisodeId = E.Id
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
                )
                GROUP BY E.ShowId
            ", new Dictionary<string, object>()
            {
                { "UserId", userId },
                { "PosterBase", SettingsHelper.Settings?.ApiDetails?.TheMovieDbOrg?.ImageBaseUrl }
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
    }
}
