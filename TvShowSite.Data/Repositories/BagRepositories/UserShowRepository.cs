using TvShowSite.Data.Common;
using TvShowSite.Data.Common.Connections;
using TvShowSite.Domain.Entities.ShowEntities;
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
                SET IsDeleted = TRUE
                WHERE UserId = @UserId
                AND ShowId = @ShowId
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
    }
}
