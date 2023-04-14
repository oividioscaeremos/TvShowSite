using TvShowSite.Data.Common;
using TvShowSite.Data.Common.Connections;
using TvShowSite.Domain.TableEntities.BagEntities;

namespace TvShowSite.Data.Repositories.BagRepositories
{
    public class ShowLanguageRepository : BaseBagRepository<ShowLanguage>
    {
        public ShowLanguageRepository(SiteDbConnection connection) : base(connection)
        {

        }

    }
}
