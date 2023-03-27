using TvShowSite.Domain.Attributes;
using TvShowSite.Domain.Common;

namespace TvShowSite.Domain.TableEntities
{
    [TableName("Language")]
    public sealed class Language : BaseEntity
    {
        public string? Name { get; set; }
    }
}
