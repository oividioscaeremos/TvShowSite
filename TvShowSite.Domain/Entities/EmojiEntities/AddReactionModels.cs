using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShowSite.Domain.Common;

namespace TvShowSite.Domain.Entities.EmojiEntities
{
    public class AddReactionRequest
    {
        public int? EpisodeId { get; set; }
        public int? CommentId { get; set; }
        public int? EmojiId { get; set; }
    }

    public class AddReactionResponse : BaseResponse
    {

    }
}
