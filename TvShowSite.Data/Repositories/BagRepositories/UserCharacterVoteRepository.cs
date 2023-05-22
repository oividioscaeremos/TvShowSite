using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShowSite.Core.Abstractions.DataAbstractions.Common;
using TvShowSite.Data.Common;
using TvShowSite.Data.Common.Connections;
using TvShowSite.Domain.TableEntities.BagEntities;

namespace TvShowSite.Data.Repositories.BagRepositories
{
    public class UserCharacterVoteRepository : BaseBagRepository<UserCharacterVote>
    {
        public UserCharacterVoteRepository(SiteDbConnection connection) : base(connection)
        {

        }

        public async Task MarkOtherVotesAsDeletedForEpisodeAndCharacterByIdAsync(int userId, int episodeId, int characterId)
        {
            await QueryAsync(@"
                UPDATE site.UserCharacterVoteRepository
                SET IsDeleted = TRUE,
                UpdateDate = NOW(),
                UpdatedBy = @UserId
                WHERE UserId = @UserId
                AND EpisodeId = @EpisodeId
                AND CharacterId = @CharacterId
                AND IsDeleted <> TRUE
            ", new Dictionary<string, object>()
            {
                { "UserId", userId },
                { "EpisodeId", episodeId },
                { "CharacterId", characterId }
            });
        }
    }
}
