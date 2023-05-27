using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShowSite.Domain.Common;

namespace TvShowSite.Domain.Entities.EmojiEntities
{
    public class GetEpisodeReactionsResponseEntity
    {
        public int EmojiId { get; set; }
        public int ReactionCount { get; set; }
        public bool IsUsersReaction { get; set; }
    }

    public class GetEpisodeReactionsResponse : BaseResponse<List<GetEpisodeReactionsResponseEntity>>
    {

    }
}
