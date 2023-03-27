using TvShowSite.Domain.Attributes;
using TvShowSite.Domain.Common;

namespace TvShowSite.Domain.TableEntities
{
    [TableName("UserTable")]
    public sealed class UserTable : BaseEntity
    {
        public string? Username { get; set; }
        public string? EmailAddress { get; set; }
        public string? ProfilePictureURL { get; set; }
        public string? CoverPictureURL { get; set; }
        public string? Password { get; set; }
    }
}
