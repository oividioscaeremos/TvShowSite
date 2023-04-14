using TvShowSite.Data.Common;
using TvShowSite.Data.Common.Connections;
using TvShowSite.Domain.TableEntities;

namespace TvShowSite.Data.Repositories
{
    public class GenreRepository : BaseRepository<Genre>
    {
        public GenreRepository(SiteDbConnection connection) : base(connection)
        {

        }

    }
}
