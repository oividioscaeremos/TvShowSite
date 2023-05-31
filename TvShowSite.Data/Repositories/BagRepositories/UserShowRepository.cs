using TvShowSite.Core.Helpers;
using TvShowSite.Data.Common;
using TvShowSite.Data.Common.Connections;
using TvShowSite.Domain.Entities.ShowEntities;
using TvShowSite.Domain.System;
using TvShowSite.Domain.TableEntities.BagEntities;

namespace TvShowSite.Data.Repositories.BagRepositories
{
    public class UserShowRepository : BaseBagRepository<UserShow>
    {
        public UserShowRepository(SiteDbConnection connection) : base(connection)
        {

        }

        public async Task<IEnumerable<int>> GetUserShowMovieDbIdsAsync(int userId)
        {
            return await QueryAsync<int>(@"
                SELECT 
                    MovieDbId
                FROM 
                    site.Show S
                    INNER JOIN site.UserShow US
                    ON US.ShowId = S.Id
                WHERE US.UserId = @UserId
                AND US.IsDeleted = FALSE
                AND S.IsDeleted = FALSE
            ", new Dictionary<string, object>()
            {
                { "UserId", userId }
            });
        }

        public async Task<UserShow> GetUserShowsByShowIdAsync(int userId, int showId)
        {
            return await QueryFirstOrDefaultAsync(@"
                SELECT * FROM site.UserShow
                WHERE UserId = @UserId
                AND ShowId = @ShowId
                AND IsDeleted <> TRUE
                LIMIT 1
            ", new Dictionary<string, object>() 
            {
                { "UserId", userId },
                { "ShowId", showId }
            });
        }

        public async Task RemoveFromUserShowsAsync(int userId, int showId)
        {
            await QueryAsync(@"
                UPDATE site.UserShow
                SET IsDeleted = TRUE,
                UpdatedBy = @UserId,
                UpdateDate = NOW()
                WHERE UserId = @UserId
                AND ShowId = @ShowId
                AND IsDeleted <> TRUE
            ", new Dictionary<string, object>()
            {
                { "UserId", userId },
                { "ShowId", showId }
            });
        }

        public async Task<IEnumerable<UserShow>> GetUserShowsByUserIdAsync(int userId)
        {
            return await QueryAsync(@"
                SELECT * FROM site.UserShow
                WHERE IsDeleted <> TRUE
                AND UserId = @UserId
            ", new Dictionary<string, object>()
            {
                { "UserId", userId }
            });
        }

        public async Task<IEnumerable<GetUserShowResponseEntity>> GetUserProfileShowsByUserIdAsync(int userId)
        {
            return await QueryAsync<GetUserShowResponseEntity>(@"
                SELECT
                    S.Id as ShowId,
                    S.Name as ShowName,
                    CONCAT(@PosterBase, S.PosterUrl) as PosterUrl
                FROM
                    site.UserShow US,
                    site.Show S
                WHERE
                    S.Id = US.ShowId
                    AND US.UserId = @UserId
                    AND US.IsDeleted <> TRUE
            ", new Dictionary<string, object>()
            {
                { "UserId", userId },
                { "PosterBase", SettingsHelper.Settings?.ApiDetails?.TheMovieDbOrg?.ImageBaseUrl ?? string.Empty }
            });
        }
    }
}
