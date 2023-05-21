using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShowSite.Domain.Common;

namespace TvShowSite.Domain.Entities.ShowEntities
{
    public class RemoveShowRequest
    {
        public int? Id { get; set; }
    }

    public class RemoveShowResponse : BaseResponse
    { 
    
    }
}
