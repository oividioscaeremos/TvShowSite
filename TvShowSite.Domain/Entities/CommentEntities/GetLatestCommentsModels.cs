using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShowSite.Domain.Common;

namespace TvShowSite.Domain.Entities.CommentEntities
{
    public class GetLatestCommentsResponseEntity
    {
        public int ShowId { get; set; }
        public int EpisodeId { get; set; }
        public string? ShowName { get; set; }
        public string? SeasonNumber { get; set; }
        public string? EpisodeNumber { get; set; }
        public string? CommentText { get; set; }
        public DateTime CommentDate { get; set; }
        public string? Username { get; set; }
    }

    public class GetLatestCommentsResponse : BaseResponse<List<GetLatestCommentsResponseEntity>>
    {
        public GetLatestCommentsResponse()
        {
            this.Value = new List<GetLatestCommentsResponseEntity>();
        }
    }
}
