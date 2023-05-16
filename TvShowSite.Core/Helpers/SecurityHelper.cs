using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TvShowSite.Core.Helpers
{
    public class SecurityHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SecurityHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public static string GetMD5Hash(string input)
        {
            using MD5 mD5 = MD5.Create();

            var data = mD5.ComputeHash(Encoding.UTF8.GetBytes(input));

            return string.Join("", data.Select(cr => cr.ToString("x2")));
        }

        public string? GetClientIPAddress()
        {
            var feature = _httpContextAccessor.HttpContext?.Features.Get<IHttpConnectionFeature>();

            return feature?.LocalIpAddress?.ToString();
        }
    }
}
