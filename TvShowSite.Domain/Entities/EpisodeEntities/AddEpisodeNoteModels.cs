using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShowSite.Domain.Common;
using TvShowSite.Domain.TableEntities.BagEntities;

namespace TvShowSite.Domain.Entities.EpisodeEntities
{
    public class AddEpisodeNoteRequest
    {
        public int? EpisodeId { get; set; }
        public string? Content { get; set; }
    }

    public class AddEpisodeNoteResponseEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int EpisodeId { get; set; }
        public string? Content { get; set; }
    }

    public class AddEpisodeNoteResponse : BaseResponse<AddEpisodeNoteResponseEntity>
    {

    }
}
