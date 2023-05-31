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

        public async Task MarkAsDeletedByUserIdAsync(int userId)
        {
            await QueryAsync(@"
                UPDATE site.UserTable
                SET IsDeleted = TRUE,
                UpdateDate = NOW(),
                UpdatedBy = @UserId
                WHERE InsertedBy = @UserId
                AND IsDeleted = FALSE
            ", new Dictionary<string, object>()
            {
                { "UserId", userId }
            });
        }

        public async Task UpdateUserProfilePictureAsync(string pictureUrl, int userId)
        {
            await QueryAsync(@"
                UPDATE site.UserTable
                SET ProfilePictureURL = @ProfilePictureURL
                WHERE Id = @Id
            ", new Dictionary<string, object>()
            {
                { "ProfilePictureURL", pictureUrl },
                { "UserId", userId }
            });
        }

        public async Task UpdateUserCoverPictureAsync(string pictureUrl, int userId)
        {
            await QueryAsync(@"
                UPDATE site.UserTable
                SET CoverPictureURL = @CoverPictureURL
                WHERE Id = @Id
            ", new Dictionary<string, object>()
            {
                { "CoverPictureURL", pictureUrl },
                { "UserId", userId }
            });
        }

        public async Task UpdateUserMailAddressAsync(string mailAddress, int userId)
        {
            await QueryAsync(@"
                UPDATE site.UserTable
                SET EmailAddress = @EmailAddress
                WHERE Id = @Id
            ", new Dictionary<string, object>()
            {
                { "EmailAddress", mailAddress },
                { "UserId", userId }
            });
        }
        
        public async Task UpdateUserPasswordAsync(string password, int userId)
        {
            await QueryAsync(@"
                UPDATE site.UserTable
                SET Password = @Password
                WHERE Id = @Id
            ", new Dictionary<string, object>()
            {
                { "Password", password },
                { "UserId", userId }
            });
        }
    }
}
