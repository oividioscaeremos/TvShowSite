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

        public async Task<string> GetEpisodeDescriptionAsync(int episodeId)
        {
            return await QueryFirstOrDefaultAsync<string>(@"
                SELECT Description FROM site.Episode
                WHERE Id = @EpisodeId
                AND IsDeleted <> TRUE
                LIMIT 1
            ", new Dictionary<string, object>()
            {
                { "EpisodeId", episodeId }
            });
        }

        public async Task<string> GetEpisodeNameAsync(int episodeId)
        {
            return await QueryFirstOrDefaultAsync<string>(@"
                SELECT
                    CONCAT('Season ', S.SeasonNumber, ' Episode ', EP.EpisodeNumber, ' - ', EP.Name)
                FROM 
                    site.Episode EP,
                    site.Season S
                WHERE EP.Id = @EpisodeId
                AND S.Id = EP.SeasonId
                LIMIT 1
            ", new Dictionary<string, object>()
            {
                { "EpisodeId", episodeId }
            });
        }

        public async Task<bool> GetEpisodeWatchedStatusAsync(int episodeId, int userId)
        {
            return await QueryFirstOrDefaultAsync<bool>(@"
                SELECT
                    COUNT(*)
                FROM site.UserEpisode
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
