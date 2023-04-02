using TvShowSite.Domain.Attributes;
using TvShowSite.Domain.Common;

namespace TvShowSite.Domain.TableEntities.BagEntities
{
    [TableName("ShowLanguage")]
    public sealed class ShowLanguage : CommonEntity
    {
        public int ShowId { get; set; }
        public int LanguageId { get; set; }
    }
}
