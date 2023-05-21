using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShowSite.Domain.Common;

namespace TvShowSite.Domain.Entities.ShowEntities
{
    public class GetUserNextToWatchResponse : BaseResponse<List<UserShowHomeEntity>>
    {


    }

    public class UserShowHomeEntity
    {
        public int ShowId { get; set; }
        public int EpisodeId { get; set; }
        public string? Name { get; set; }
        public string? PosterURL { get; set; }
        public int SeasonNumber { get; set; }
        public int EpisodeNumber { get; set; }
    }
}
