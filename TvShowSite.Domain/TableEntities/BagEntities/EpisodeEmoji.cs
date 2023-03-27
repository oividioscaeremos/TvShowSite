using TvShowSite.Domain.Common;

namespace TvShowSite.Domain.TableEntities.BagEntities
{
    public sealed class EpisodeEmoji : CommonEntity
    {
        public int EpisodeId { get; set; }
        public int EmojiId { get; set; }
        public int UserId { get; set; }
    }
}
