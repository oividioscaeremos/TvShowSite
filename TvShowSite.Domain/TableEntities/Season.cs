using TvShowSite.Domain.Attributes;
using TvShowSite.Domain.Common;

namespace TvShowSite.Domain.TableEntities
{
    [TableName("Season")]
    public sealed class Season : BaseEntity
    {
        public int? MovieDbId { get; set; }
        public int ShowId { get; set; }
        public string? Name { get; set; }
        public int SeasonNumber { get; set; }
        public string? Description { get; set; }
        public string? PosterURL { get; set; }
        public DateTime? AirDate { get; set; }
    }
}
