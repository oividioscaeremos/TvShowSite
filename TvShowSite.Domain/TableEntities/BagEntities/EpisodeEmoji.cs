using TvShowSite.Domain.Attributes;
using TvShowSite.Domain.Common;

namespace TvShowSite.Domain.TableEntities.BagEntities
{
    [TableName("EpisodeEmoji")]
    public sealed class EpisodeEmoji : CommonEntity
    {
        public int EpisodeId { get; set; }
        public int EmojiId { get; set; }
        public int UserId { get; set; }
    }
}
