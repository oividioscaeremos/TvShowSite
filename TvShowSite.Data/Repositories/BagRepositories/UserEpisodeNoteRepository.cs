using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShowSite.Core.Abstractions.DataAbstractions.Common;
using TvShowSite.Data.Common;
using TvShowSite.Data.Common.Connections;
using TvShowSite.Domain.Entities.ShowEntities;
using TvShowSite.Domain.TableEntities.BagEntities;

namespace TvShowSite.Data.Repositories.BagRepositories
{
    public class UserEpisodeNoteRepository : BaseRepository<UserEpisodeNote>
    {
        public UserEpisodeNoteRepository(SiteDbConnection connection) : base(connection)
        {

        }

        public async Task<IEnumerable<UserEpisodeNote>> GetAllNotesByEpisodeIdAsync(int episodeId, int userId)
        {
            return await QueryAsync(@"
                SELECT * FROM site.UserEpisodeNote
                WHERE UserId = @UserId
                AND EpisodeId = @EpisodeId
                AND IsDeleted <> TRUE
            ", new Dictionary<string, object>()
            {
                { "UserId", userId },
                { "EpisodeId", episodeId }
            });
        }

        public async Task<IEnumerable<UserEpisodeNote>> GetAllNotesByUserIdAsync(int userId)
        {
            return await QueryAsync(@"
                SELECT * FROM site.UserEpisodeNote
                WHERE UserId = @UserId
                AND IsDeleted <> TRUE
            ", new Dictionary<string, object>()
            {
                { "UserId", userId }
            });
        }

        public async Task UpdateNoteContentByIdAsync(int noteId, string newContent, int userId)
        {
            await QueryAsync(@"
                UPDATE site.UserEpisodeNote
                SET Content = @Content,
                UpdateDate = NOW(),
                UpdatedBy = @UserId
                WHERE Id = @NoteId
            ", new Dictionary<string, object>()
            {
                { "NoteId", noteId },
                { "UserId", userId },
                { "Content", newContent }
            });
        }

        public async Task<IEnumerable<GetShowNotesDbRow>> GetShowNotesAsync(int? showId, int userId)
        {
            return await QueryAsync<GetShowNotesDbRow>($@"
                SELECT
                    S.Id as ShowId,
                    E.Id as EpisodeId,
                    S.Name as ShowName,
                    SE.SeasonNumber,
                    E.EpisodeNumber,
                    UEN.Content as NoteContent
                FROM
                    site.UserEpisodeNote UEN,
                    site.Episode E,
                    site.Show S,
                    site.Season SE
                WHERE
                    E.Id = UEN.EpisodeId
                    AND S.Id = E.ShowId
                    AND SE.Id = E.SeasonId
                    AND UEN.UserId = @UserId
                    AND UEN.IsDeleted <> TRUE
                    {(showId.HasValue ? "AND S.Id = @ShowId" : "")}
                ORDER BY SE.SeasonNumber ASC, E.EpisodeNumber ASC
            ", new Dictionary<string, object>()
            {
                { "UserId", userId },
                { "ShowId", showId ?? -1 }
            });
        }
    }
}
