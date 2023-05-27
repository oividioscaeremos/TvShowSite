using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShowSite.Domain.Common;

namespace TvShowSite.Domain.Entities.EmojiEntities
{
    public class GetCommentReactionsResponseEntity
    {
        public int EmojiId { get; set; }
        public int ReactionCount { get; set; }
        public bool IsUsersReaction { get; set; }
        public string? EmojiClassName { get; set; }
    }

    public class GetCommentReactionsResponse : BaseResponse<List<GetCommentReactionsResponseEntity>>
    {

    }
}
