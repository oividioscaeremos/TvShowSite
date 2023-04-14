using TvShowSite.Data.Common;
using TvShowSite.Data.Common.Connections;
using TvShowSite.Domain.TableEntities;

namespace TvShowSite.Data.Repositories
{
    public class CommentRepository : BaseRepository<Comment>
    {
        public CommentRepository(SiteDbConnection connection) : base(connection)
        {

        }

    }
}
