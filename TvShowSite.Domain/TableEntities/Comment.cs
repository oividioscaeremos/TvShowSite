using TvShowSite.Domain.Attributes;
using TvShowSite.Domain.Common;

namespace TvShowSite.Domain.TableEntities
{
    [TableName("Comment")]
    public sealed class Comment : BaseEntity
    {
        public int? ParentCommentId { get; set; }
        public string? CommentText { get; set; }
        public int ShowId { get; set; }
        public int SeasonId { get; set; }
        public int EpisodeId { get; set; }
    }
}
