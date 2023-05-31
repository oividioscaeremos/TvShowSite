using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShowSite.Domain.Common;

namespace TvShowSite.Domain.Entities.AccountEntities
{
    public class ChangePasswordRequest
    {
        public string? Password { get; set; }
    }

    public class ChangePasswordResponse : BaseResponse
    {

    }
}
