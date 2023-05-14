using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShowSite.Domain.Common;

namespace TvShowSite.Domain.Entities.AccountEntities
{
    public class RefreshTokenRequest
    {
        public string? RefreshToken { get; set; }
    }

    public class RefreshTokenResponse : BaseResponse<LoginResponseEntity>
    {

    }
}
