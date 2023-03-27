using TvShowSite.Domain.Common;

namespace TvShowSite.Domain.TableEntities.BagEntities
{
    public sealed class ShowLanguage : CommonEntity
    {
        public int ShowId { get; set; }
        public int LanguageId { get; set; }
    }
}
