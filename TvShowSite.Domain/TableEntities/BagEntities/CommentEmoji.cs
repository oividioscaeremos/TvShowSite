using TvShowSite.Domain.Attributes;
using TvShowSite.Domain.Common;

namespace TvShowSite.Domain.TableEntities.BagEntities
{
    [TableName("CommentEmoji")]
    public sealed class CommentEmoji : CommonEntity
    {
        public int CommentId { get; set; }
        public int EmojiId { get; set; }
        public int UserId { get; set; }
    }
}
