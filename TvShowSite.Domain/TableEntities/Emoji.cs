using TvShowSite.Domain.Common;

namespace TvShowSite.Domain.TableEntities
{
    public class Emoji : BaseEntity
    {
        public string? EmojiClassName { get; set; }
        public string? EmojiIconURL { get; set; }
        public bool IsComment { get; set; }
        public bool IsShow { get; set; }
    }
}
