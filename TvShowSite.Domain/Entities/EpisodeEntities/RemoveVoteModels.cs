using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShowSite.Domain.Common;

namespace TvShowSite.Domain.Entities.EpisodeEntities
{
    public class RemoveVoteRequest
    {
        public int? EpisodeId { get; set; }
    }

    public class RemoveVoteResponse : BaseResponse
    {

    }
}
