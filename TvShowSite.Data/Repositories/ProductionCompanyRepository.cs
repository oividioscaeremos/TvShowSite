using TvShowSite.Data.Common;
using TvShowSite.Data.Common.Connections;
using TvShowSite.Domain.TableEntities;

namespace TvShowSite.Data.Repositories
{
    public class ProductionCompanyRepository : BaseRepository<ProductionCompany>
    {
        public ProductionCompanyRepository(SiteDbConnection connection) : base(connection)
        {

        }

        public async Task<ProductionCompany> GetByNameAsync(string name)
        {
            return await QueryFirstOrDefaultAsync(@"
                SELECT * FROM site.ProductionCompany
                WHERE Name = @Name
                LIMIT 1
            ", new Dictionary<string, object>()
            {
                { "Name", name }
            });
        }
    }
}
