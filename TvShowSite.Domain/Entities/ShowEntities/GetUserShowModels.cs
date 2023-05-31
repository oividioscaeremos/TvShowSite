using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShowSite.Domain.Common;

namespace TvShowSite.Domain.Entities.ShowEntities
{
    public class GetUserShowResponseEntity
    {
        public int ShowId { get; set; }
        public string? ShowName { get; set; }
        public string? PosterUrl { get; set; }
    }

    public class GetUserShowResponse : BaseResponse<List<GetUserShowResponseEntity>>
    {
        public GetUserShowResponse()
        {
            this.Value = new List<GetUserShowResponseEntity>();
        }
    }
}
