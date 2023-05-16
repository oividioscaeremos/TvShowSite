using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShowSite.Domain.Common;

namespace TvShowSite.Domain.Entities.AccountEntities
{
    public class RegisterRequest
    {
        public string? Username { get; set; }
        public string? EmailAddress { get; set; }
        public string? Password { get; set; }
    }

    public class RegisterResponse : BaseResponse
    {

    }
}
