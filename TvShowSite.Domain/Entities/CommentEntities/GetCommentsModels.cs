using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShowSite.Domain.Common;

namespace TvShowSite.Domain.Entities.CommentEntities
{
    public class GetCommentsRequest
    {
        public int? ShowId { get; set; }
        public int? EpisodeId { get; set; }
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
    }

    public class GetCommentsResponseEntity
    {
        public int Id { get; set; }
        public string? CommentText { get; set; }
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? UserProfilePicture { get; set; }
        public bool IsUsersComment { get; set; }
        public DateTime CommentDate { get; set; }
    }

    public class GetCommentsResponse : BaseResponse<List<GetCommentsResponseEntity>>
    {

    }
}
