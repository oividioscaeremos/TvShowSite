using TvShowSite.Domain.Common;

namespace TvShowSite.Domain.TableEntities
{
    public sealed class Genre : BaseEntity
    {
        public string? MovieDbId { get; set; }
        public string? Name { get; set; }
    }
}
