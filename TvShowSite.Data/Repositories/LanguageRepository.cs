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

        public async Task<Language> GetByNameAsync(string name)
        {
            return await QueryFirstOrDefaultAsync(@"
                SELECT * FROM site.Language
                WHERE Name = @Name
                LIMIT 1
            ", new Dictionary<string, object>()
            {
                { "Name", name }
            });
        }
    }
}
