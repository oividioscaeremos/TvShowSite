using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShowSite.Domain.Common;

namespace TvShowSite.Domain.Entities.CommentEntities
{
    public class AddCommentRequest
    {
        public int? ShowId { get; set; }
        public int? EpisodeId { get; set; }
        public int? ParentCommentId { get; set; }
        public string? Comment { get; set; }
    }

    public class AddCommentResponse : BaseResponse
    {

    }
}
