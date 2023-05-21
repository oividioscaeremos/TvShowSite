using TvShowSite.Data.Common;
using TvShowSite.Data.Common.Connections;
using TvShowSite.Domain.TableEntities;

namespace TvShowSite.Data.Repositories
{
    public class CountryRepository : BaseRepository<Country>
    {
        public CountryRepository(SiteDbConnection connection) : base(connection)
        {

        }

        public async Task<Country> GetByNameAsync(string name)
        {
            return await QueryFirstOrDefaultAsync(@"
                SELECT * FROM site.Country
                WHERE Name = @Name
                LIMIT 1
            ", new Dictionary<string, object>()
            {
                { "Name", name }
            });
        }
    }
}
