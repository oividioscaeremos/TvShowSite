using TvShowSite.Data.Common;
using TvShowSite.Data.Common.Connections;
using TvShowSite.Domain.TableEntities;

namespace TvShowSite.Data.Repositories
{
    public class ShowRepository : BaseRepository<Show>
    {
        public ShowRepository(SiteDbConnection connection) : base(connection)
        {

        }

        public async Task<IEnumerable<Show>> GetShowsByNameAsync(string name, int? page, int? pageSize)
        {
            if(page is null) page = 1;
            if(pageSize is null) pageSize = int.MaxValue;

            var nameVariants = name.Split(' ').Select(name => " Name LIKE '%" + name.Trim().ToLower() + "%'");

            return await QueryAsync($@"
                SELECT * FROM site.Show
                WHERE Name = @Name
                OR ({string.Join(" OR ", nameVariants)})
                ORDER BY Popularity DESC
                LIMIT @PageSize
                OFFSET @SkipCount
            ", new Dictionary<string, object> 
            {
                { "PageSize", pageSize },
                { "SkipCount", (page - 1) * pageSize }
            });
        }

        public async Task<Show> GetShowByMovieDbOrgIdAsync(string movieDbId)
        {
            return await QueryFirstOrDefaultAsync(@"
                SELECT * FROM site.Show
                WHERE MovieDbId = @MovieDbId
            ", new Dictionary<string, object>()
            {
                { "MovieDbId", movieDbId }
            });
        }
    }
}
