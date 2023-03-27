using TvShowSite.Domain.Common;

namespace TvShowSite.Domain.TableEntities.BagEntities
{
    public sealed class EpisodeImage : CommonEntity
    {
        public string? EpisodeId { get; set; }
        public string? ImageURL { get; set; }
    }
}
