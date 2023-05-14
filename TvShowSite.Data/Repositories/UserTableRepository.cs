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

        public async Task<UserTable> GetUserByMailAddressAsync(string mailAddress)
        {
            return await QueryFirstOrDefaultAsync(@"
                SELECT * FROM site.UserTable
                WHERE EmailAddress = @MailAddress
                AND IsDeleted = false
                LIMIT 1
            ", new Dictionary<string, object>()
            {
                { "MailAddress", mailAddress }
            });
        }

        public async Task<UserTable> GetUserByUsernameAsync(string username)
        {
            return await QueryFirstOrDefaultAsync(@"
                SELECT * FROM site.UserTable
                WHERE Username = @Username
                AND IsDeleted = false
                LIMIT 1
            ", new Dictionary<string, object>()
            {
                { "Username", username }
            });
        }

        public async Task<UserTable> GetUserByUsernameAndEncryptedPasswordAsync(string userName, string password)
        {
            return await QueryFirstOrDefaultAsync(@"
                SELECT * FROM site.UserTable
                WHERE Username = @Username
                AND Password = @Password
                AND IsDeleted = false
            ", new Dictionary<string, object>()
            {
                { "Username", userName },
                { "Password", password }
            });
        }
    }
}
