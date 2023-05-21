using TvShowSite.Data.Common;
using TvShowSite.Data.Common.Connections;
using TvShowSite.Domain.TableEntities;

namespace TvShowSite.Data.Repositories
{
    public class EpisodeRepository : BaseRepository<Episode>
    {
        public EpisodeRepository(SiteDbConnection connection) : base(connection)
        {

        }

        public async Task<Episode> GetByMovieDbIdAsync(string movieDbId)
        {
            return await QueryFirstOrDefaultAsync(@"
                SELECT * FROM site.Episode
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
