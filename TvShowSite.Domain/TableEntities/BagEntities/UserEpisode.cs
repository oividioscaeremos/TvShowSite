using TvShowSite.Domain.Common;

namespace TvShowSite.Domain.TableEntities.BagEntities
{
    public sealed class UserEpisode : CommonEntity
    {
        public int UserId { get; set; }
        public int EpisodeId { get; set; }
    }
}
