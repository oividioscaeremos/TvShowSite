using TvShowSite.Domain.Common;

namespace TvShowSite.Domain.TableEntities
{
    public sealed class Person : BaseEntity
    {
        public string? MovideDbId { get; set; }
        public int ShowId { get; set; }
        public int PersonId { get; set; }
        public string? CharacterName { get; set; }
        public string? Role { get; set; }
        public int? EpisodeCount { get; set; }
        public string? PosterURL { get; set; }
    }
}
