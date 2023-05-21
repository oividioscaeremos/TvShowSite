using TvShowSite.Data.Common;
using TvShowSite.Data.Common.Connections;
using TvShowSite.Domain.TableEntities.BagEntities;

namespace TvShowSite.Data.Repositories.BagRepositories
{
    public class CharacterEpisodeRepository : BaseBagRepository<CharacterEpisode>
    {
        public CharacterEpisodeRepository(SiteDbConnection connection) : base(connection)
        {

        }

        public async Task<CharacterEpisode> GetByCharacterIdSeasonIdEpisodeIdAsync(int characterId, int seasonId, int episodeId)
        {
            return await QueryFirstOrDefaultAsync(@"
                SELECT * FROM site.CharacterEpisode
                WHERE CharacterId = @CharacterId
                AND SeasonId = @SeasonId
                AND EpisodeId = @EpisodeId
                AND IsDeleted <> TRUE
                LIMIT 1
            ", new Dictionary<string, object>()
            {
                { "CharacterId", characterId },
                { "SeasonId", seasonId },
                { "EpisodeId", episodeId },
            });
        }
    }
}
