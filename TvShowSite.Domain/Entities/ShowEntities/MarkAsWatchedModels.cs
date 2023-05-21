using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShowSite.Domain.Common;

namespace TvShowSite.Domain.Entities.ShowEntities
{
    public class MarkAsWatchedRequest
    {
        public int? ShowId { get; set; }
        public int? EpisodeId { get; set; }
    }

    public class MarkAsWatchedResponseEntity
    {
        public int EpisodeId { get; set; }
        public int SeasonNumber { get; set; }
        public int EpisodeNumber { get; set; }
        public bool IsFinished { get; set; }
    }

    public class MarkAsWatchedResponse : BaseResponse<MarkAsWatchedResponseEntity>
    {

    }
}
