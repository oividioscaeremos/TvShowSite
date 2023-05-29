using TvShowSite.Domain.Attributes;
using TvShowSite.Domain.Common;

namespace TvShowSite.Domain.TableEntities
{
    [TableName("Show")]
    public sealed class Show : BaseEntity
    {
        public int? MovieDbId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? CountryId { get; set; }
        public string? OriginalName { get; set; }
        public int? OriginalLanguageId { get; set; }
        public string? PosterURL { get; set; }
        public DateTime? FirstAirDate { get; set; }
        public DateTime? LastAirDate { get; set; }
        public bool IsOngoing { get; set; }
        public int? ProductionCompanyId { get; set; }
        public int EpisodeRunTime { get; set; }
        public int? Popularity { get; set; }
    }
}
