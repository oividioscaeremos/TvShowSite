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
    public class AuthorizationRepository : BaseRepository<Authorization>
    {
        public AuthorizationRepository(SiteDbConnection connection) : base(connection)
        {

        }

        public async Task<Authorization> GetByRefreshTokenAndUsernameAsync(string refreshToken, string username)
        {
            return await QueryFirstOrDefaultAsync(@"
                SELECT * FROM site.Authorization
                WHERE RefreshTokenExpiresAt > NOW()
                AND Username = @Username
                AND RefreshToken = @RefreshToken
                AND IsDeleted = false
                LIMIT 1
            ", new Dictionary<string, object>()
            {
                { "Username", username },
                { "RefreshToken", refreshToken }
            });
        }
    }
}
