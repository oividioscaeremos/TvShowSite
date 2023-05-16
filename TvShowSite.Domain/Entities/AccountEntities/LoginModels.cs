using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShowSite.Domain.Common;

namespace TvShowSite.Domain.Entities.AccountEntities
{
    public class LoginRequest
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }

    public class LoginResponseEntity
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }

    public class LoginResponse : BaseResponse<LoginResponseEntity>
    {

    }
}
