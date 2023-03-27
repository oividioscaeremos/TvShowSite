using TvShowSite.Domain.Common;

namespace TvShowSite.Domain.TableEntities.BagEntities
{
    public sealed class UserShow : CommonEntity
    {
        public int UserId { get; set; }
        public int ShowId { get; set; }
        public bool IsArchieved { get; set; }
    }
}
