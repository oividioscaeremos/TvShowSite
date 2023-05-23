using TvShowSite.Core.Helpers;
using TvShowSite.Data.Common;
using TvShowSite.Data.Common.Connections;
using TvShowSite.Domain.Entities.ShowEntities;
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

        public async Task<IEnumerable<FavoriteCharactersResponsEntity>> GetCharactersByShowIdAndEpisodeIdAsync(int showId, int? episodeId)
        {
            return await QueryAsync<FavoriteCharactersResponsEntity>($@"
                SELECT * FROM (
                    SELECT
                        CONCAT(@PosterBaseUrl, C.PosterURL) as PosterURL,
                        MAX(C.CharacterName) as CharacterName,
                        (
                            SELECT COUNT(*) 
                            FROM site.UserCharacterVote UCV 
                            WHERE UCV.CharacterId = C.Id 
                            AND UCV.IsDeleted <> TRUE
                        ) as VoteCount,
                        MAX(C.CharacterOrder) as CharacterOrder
                    FROM
                        site.CharacterEpisode CE,
                        site.Character C,
                        site.Episode E
                    WHERE
                        E.Id = CE.EpisodeId
                        AND C.Id = CE.CharacterId
                        AND E.ShowId = @ShowId
                    GROUP BY {(episodeId.HasValue ? "E.Id" : "E.ShowId")}, C.Id
                ) X
                ORDER BY X.VoteCount DESC, X.CharacterOrder ASC
            ", new Dictionary<string, object>()
            {
                { "ShowId", showId },
                { "EpisodeId", episodeId ?? -1 },
                { "PosterBaseUrl", SettingsHelper.Settings?.ApiDetails?.TheMovieDbOrg?.ImageBaseUrl ?? "" }
            });
        }
    }
}
