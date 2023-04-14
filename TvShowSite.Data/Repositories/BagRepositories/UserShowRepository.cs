using TvShowSite.Data.Common;
using TvShowSite.Data.Common.Connections;
using TvShowSite.Domain.TableEntities.BagEntities;

namespace TvShowSite.Data.Repositories.BagRepositories
{
    public class UserShowRepository : BaseBagRepository<UserShow>
    {
        public UserShowRepository(SiteDbConnection connection) : base(connection)
        {

        }

    }
}
