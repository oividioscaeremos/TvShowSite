using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShowSite.Domain.Common;

namespace TvShowSite.Domain.Entities.AccountEntities
{
    public class ChangeUserPictureRequest
    {
        public string? PictureUrl { get; set; }
        public bool? IsCoverPicture { get; set; }
        public bool? IsProfilePicture { get; set; }
    }

    public class ChangeUserPictureResponse : BaseResponse
    {

    }
}
