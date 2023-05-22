using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShowSite.Domain.Common;

namespace TvShowSite.Domain.Entities.EpisodeEntities
{
    public class AddVoteRequest
    {
        public int? EpisodeId { get; set; }
        public int? CharacterId { get; set; }
    }

    public class AddVoteResponse : BaseResponse
    {

    }
}
