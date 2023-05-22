using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShowSite.Domain.Common;

namespace TvShowSite.Domain.Entities.EpisodeEntities
{
    public class ShowNextToWatchResponseEntity
    {
        public int SeasonNumber { get; set; }
        public int EpisodeNumber { get; set; }
    }

    public class ShowNextToWatchResponse : BaseResponse<ShowNextToWatchResponseEntity>
    {

    }
}
