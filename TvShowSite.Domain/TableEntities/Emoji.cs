using TvShowSite.Domain.Attributes;
using TvShowSite.Domain.Common;

namespace TvShowSite.Domain.TableEntities
{
    [TableName("Emoji")]
    public class Emoji : BaseEntity
    {
        public string? Name { get; set; }
        public string? EmojiClassName { get; set; }
        public string? EmojiIconURL { get; set; }
        public bool IsComment { get; set; }
        public bool IsShow { get; set; }
    }
}
