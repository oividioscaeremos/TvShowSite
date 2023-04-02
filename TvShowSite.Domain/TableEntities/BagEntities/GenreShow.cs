using TvShowSite.Domain.Attributes;
using TvShowSite.Domain.Common;

namespace TvShowSite.Domain.TableEntities.BagEntities
{
    [TableName("GenreShow")]
    public sealed class GenreShow : CommonEntity
    {
        public int GenreId { get; set; }
        public int ShowId { get; set; }
    }
}
