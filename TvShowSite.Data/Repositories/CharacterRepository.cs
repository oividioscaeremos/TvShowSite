using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShowSite.Core.Abstractions.DataAbstractions.Common;
using TvShowSite.Data.Common;
using TvShowSite.Data.Common.Connections;
using TvShowSite.Domain.TableEntities;

namespace TvShowSite.Data.Repositories
{
    public class CharacterRepository : BaseRepository<Character>
    {
        public CharacterRepository(SiteDbConnection connection) : base(connection)
        {

        }

        public async Task<Character> GetCharacterFromMovideDbIdAsync(int movieDbId)
        {
            return await QueryFirstOrDefaultAsync(@"
                SELECT * FROM site.Character
                WHERE MovieDbId = @MovieDbId
                LIMIT 1
            ", new Dictionary<string, object>()
            {
                { "MovieDbId", movieDbId }
            });
        }
            });
        }
    }
}
