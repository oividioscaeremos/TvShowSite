using TvShowSite.Data.Common;
using TvShowSite.Data.Common.Connections;
using TvShowSite.Domain.TableEntities;

namespace TvShowSite.Data.Repositories
{
    public class SeasonRepository : BaseRepository<Season>
    {
        public SeasonRepository(SiteDbConnection connection) : base(connection)
        {

        }

        public async Task<Season> GetByMovieDbIdAsync(int movieDbId)
        {
            return await QueryFirstOrDefaultAsync(@"
                SELECT * FROM site.Season
                WHERE MovieDbId = @MovieDbId
                AND IsDeleted <> TRUE
                LIMIT 1
            ", new Dictionary<string, object>()
            {
                { "MovieDbId", movieDbId }
            });
        }
    }
}
