using TvShowSite.Data.Common;
using TvShowSite.Data.Common.Connections;
using TvShowSite.Domain.TableEntities.BagEntities;

namespace TvShowSite.Data.Repositories.BagRepositories
{
    public class GenreShowRepository : BaseBagRepository<GenreShow>
    {
        public GenreShowRepository(SiteDbConnection connection) : base(connection)
        {

        }

    }
}
