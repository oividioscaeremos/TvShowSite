using TvShowSite.Domain.Attributes;
using TvShowSite.Domain.Common;

namespace TvShowSite.Domain.TableEntities
{
    [TableName("Country")]
    public sealed class Country : BaseEntity
    {
        public string? Name { get; set; }
    }
}
