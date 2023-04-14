using TvShowSite.Data.Common;
using TvShowSite.Data.Common.Connections;
using TvShowSite.Domain.TableEntities;

namespace TvShowSite.Data.Repositories
{
    public class LanguageRepository : BaseRepository<Language>
    {
        public LanguageRepository(SiteDbConnection connection) : base(connection)
        {

        }

    }
}
