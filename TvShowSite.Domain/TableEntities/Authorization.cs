using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShowSite.Domain.Attributes;
using TvShowSite.Domain.Common;

namespace TvShowSite.Domain.TableEntities
{
    [TableName("Authorization")]
    public class Authorization : BaseEntity
    {
        public string? Username { get; set; }
        public string? IPAddress { get; set; }
        public bool DidSucceed { get; set; }
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? AccessTokenExpiresAt { get; set; }
        public DateTime? RefreshTokenExpiresAt { get; set; }
    }
}
