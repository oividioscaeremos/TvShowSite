using TvShowSite.Domain.Attributes;
using TvShowSite.Domain.Common;

namespace TvShowSite.Domain.TableEntities
{
    [TableName("ProductionCompany")]
    public sealed class ProductionCompany : BaseEntity
    {
        public string? Name { get; set; }
    }
}
