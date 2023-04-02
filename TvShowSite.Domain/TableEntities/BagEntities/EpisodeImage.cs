using TvShowSite.Domain.Attributes;
using TvShowSite.Domain.Common;

namespace TvShowSite.Domain.TableEntities.BagEntities
{
    [TableName("EpisodeImage")]
    public sealed class EpisodeImage : CommonEntity
    {
        public string? EpisodeId { get; set; }
        public string? ImageURL { get; set; }
    }
}
