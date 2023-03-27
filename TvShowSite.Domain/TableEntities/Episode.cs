using TvShowSite.Domain.Attributes;
using TvShowSite.Domain.Common;

namespace TvShowSite.Domain.TableEntities
{
    [TableName("Episode")]
    public sealed class Episode : BaseEntity
    {
        public string? MoviedbId { get; set; }
        public int ShowId { get; set; }
        public int SeasonId { get; set; }
        public int EpisodeNumber { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? PosterURL { get; set; }
        public DateTime? AirDate { get; set; }
    }
}
