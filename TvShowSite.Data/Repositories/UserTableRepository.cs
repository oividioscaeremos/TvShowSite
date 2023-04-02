using TvShowSite.Data.Common;
using TvShowSite.Data.Common.Connections;
using TvShowSite.Domain.TableEntities;

namespace TvShowSite.Data.Repositories
{
    public class UserTableRepository : BaseRepository<UserTable>
    {
        public UserTableRepository(SiteDbConnection connection) : base(connection)
        {

        }
    }
}
