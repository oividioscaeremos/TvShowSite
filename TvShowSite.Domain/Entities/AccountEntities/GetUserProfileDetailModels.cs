using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShowSite.Domain.Common;

namespace TvShowSite.Domain.Entities.AccountEntities
{
    public class GetUserProfileDetailRequest
    {
        public int? UserId { get; set; }
    }

    public class GetUserProfileDetailResponseEntity
    {
        public bool IsUsersOwnProfile { get; set; }
        public string? CoverPictureUrl { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string? MailAddress { get; set; }
        public string? Username { get; set; }
    }

    public class GetUserProfileDetailResponse : BaseResponse<GetUserProfileDetailResponseEntity>
    {

    }
}
