using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShowSite.Domain.Common;

namespace TvShowSite.Domain.Entities.ShowEntities
{
    public class GetShowNotesShowEntity
    {
        public int ShowId { get; set; }
        public string? ShowName { get; set; }
        public List<GetShowNotesNoteEntity> Notes { get; set; }

        public GetShowNotesShowEntity()
        {
            this.Notes = new List<GetShowNotesNoteEntity>();
        }
    }

    public class GetShowNotesNoteEntity
    {
        public int SeasonNumber { get; set; }
        public int EpisodeNumber { get; set; }
        public int EpisodeId { get; set; }
        public string? NoteContent { get; set; }
    }

    public class GetShowNotesResponse : BaseResponse<List<GetShowNotesShowEntity>>
    {
        public GetShowNotesResponse()
        {
            this.Value = new List<GetShowNotesShowEntity>();
        }
    }

    public class GetShowNotesDbRow
    {
        public int ShowId { get; set; }
        public int EpisodeId { get; set; }
        public string? ShowName { get; set; }
        public int SeasonNumber { get; set; }
        public int EpisodeNumber { get; set; }
        public string? NoteContent { get; set; }
    }
}
