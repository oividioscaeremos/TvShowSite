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

        public async Task<IEnumerable<FavoriteCharactersResponsEntity>> GetCharactersByShowIdAndEpisodeIdAsync(int showId, int? episodeId, int userId)
        {
            return await QueryAsync<FavoriteCharactersResponsEntity>($@"
                SELECT * FROM (
                    SELECT
                        C.Id as CharacterId,
                        CONCAT(@PosterBaseUrl, C.PosterURL) as PosterURL,
                        MAX(C.CharacterName) as CharacterName,
                        (
                            SELECT COUNT(*) 
                            FROM site.UserCharacterVote UCV 
                            WHERE UCV.CharacterId = C.Id 
                            AND UCV.IsDeleted <> TRUE
                            {(episodeId.HasValue ? "AND UCV.EpisodeId = MAX(E.Id)" : "")}
                        ) as VoteCount,
                        MAX(C.CharacterOrder) as CharacterOrder,
                        CASE
                            WHEN (
                                SELECT COUNT(*) FROM site.UserCharacterVote UCV2 
                                WHERE UCV2.CharacterId = C.Id 
                                AND UCV2.UserId = @UserId 
                                AND UCV2.IsDeleted <> TRUE
                                {(episodeId.HasValue ? "AND UCV2.EpisodeId = MAX(E.Id)" : "")}
                            ) > 0 THEN TRUE
                            ELSE FALSE
                        END as IsUsersVote
                    FROM
                        site.CharacterEpisode CE,
                        site.Character C,
                        site.Episode E
                    WHERE
                        E.Id = CE.EpisodeId
                        AND C.Id = CE.CharacterId
                        AND E.ShowId = @ShowId
                        {(episodeId.HasValue ? "AND E.Id = @EpisodeId" : "")}
                    GROUP BY {(episodeId.HasValue ? "E.Id" : "E.ShowId")}, C.Id
                ) X
                ORDER BY X.VoteCount DESC, X.CharacterOrder ASC
            ", new Dictionary<string, object>()
            {
                { "ShowId", showId },
                { "EpisodeId", episodeId ?? -1 },
                { "PosterBaseUrl", SettingsHelper.Settings?.ApiDetails?.TheMovieDbOrg?.ImageBaseUrl ?? "" },
                { "UserId", userId }
            });
        }
    }
}
