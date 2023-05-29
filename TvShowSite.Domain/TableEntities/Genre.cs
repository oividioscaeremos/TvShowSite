using TvShowSite.Domain.Attributes;
using TvShowSite.Domain.Common;

namespace TvShowSite.Domain.TableEntities
{
    [TableName("Genre")]
    public sealed class Genre : BaseEntity
    {
        public int? MovieDbId { get; set; }
        public string? Name { get; set; }
    }
}
