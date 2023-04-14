using TvShowSite.Data.Common;
using TvShowSite.Data.Common.Connections;
using TvShowSite.Domain.TableEntities;

namespace TvShowSite.Data.Repositories
{
    public class PersonRepository : BaseRepository<Person>
    {
        public PersonRepository(SiteDbConnection connection) : base(connection)
        {

        }

    }
}
