using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShowSite.Domain.Common;

namespace TvShowSite.Domain.Entities.EpisodeEntities
{
    public class GetEpisodeNotesResponseEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int EpisodeId { get; set; }
        public string? Content { get; set; }
    }

    public class GetEpisodeNotesResponse : BaseResponse<List<GetEpisodeNotesResponseEntity>>
    {
        public GetEpisodeNotesResponse()
        {
            this.Value = new List<GetEpisodeNotesResponseEntity>();
        }
    }
}
