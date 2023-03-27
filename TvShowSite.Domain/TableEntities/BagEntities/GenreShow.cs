using TvShowSite.Domain.Common;

namespace TvShowSite.Domain.TableEntities.BagEntities
{
    public sealed class GenreShow : CommonEntity
    {
        public int GenreId { get; set; }
        public int ShowId { get; set; }
    }
}
