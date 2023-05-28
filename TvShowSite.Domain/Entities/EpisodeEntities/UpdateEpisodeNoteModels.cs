using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShowSite.Domain.Common;

namespace TvShowSite.Domain.Entities.EpisodeEntities
{
    public class UpdateEpisodeNoteRequest
    {
        public int? NoteId { get; set; }
        public string? NewContent { get; set; }
    }

    public class UpdateEpisodeNoteResponse : BaseResponse
    {

    }
}
