using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TvShowSite.Domain.Entities.CommentEntities
{
    public class GetChildCommentsRequest
    {
        public int? CommentId { get; set; }
    }

    public class GetChildCommentsResponse : GetCommentsResponse
    {

    }
}
