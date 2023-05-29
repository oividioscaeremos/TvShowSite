using TvShowSite.Core.Helpers;
using TvShowSite.Data.Common;
using TvShowSite.Data.Common.Connections;
using TvShowSite.Domain.Entities.ShowEntities;
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

        public async Task<Show> GetShowByMovieDbOrgIdAsync(int movieDbId)
        {
            return await QueryFirstOrDefaultAsync(@"
                SELECT * FROM site.Show
                WHERE MovieDbId = @MovieDbId
            ", new Dictionary<string, object>()
            {
                { "MovieDbId", movieDbId }
            });
        }

        public async Task<string?> GetShowDescriptionAsync(int id)
        {
            return await QueryFirstOrDefaultAsync<string?>(@"
                SELECT Description FROM site.Show
                WHERE Id = @Id
            ", new Dictionary<string, object>()
            {
                { "Id", id }
            });
        }

        public async Task<string?> GetShowNameAsync(int id)
        {
            return await QueryFirstOrDefaultAsync<string?>(@"
                SELECT Name FROM site.Show
                WHERE Id = @Id
            ", new Dictionary<string, object>()
            {
                { "Id", id }
            });
        }

        public async Task<string?> GetShowPosterURLAsync(int id)
        {
            return await QueryFirstOrDefaultAsync<string?>(@"
                SELECT CONCAT(@BaseURL, PosterURL) FROM site.Show
                WHERE Id = @Id
            ", new Dictionary<string, object>()
            {
                { "Id", id },
                { "BaseURL", SettingsHelper.Settings?.ApiDetails?.TheMovieDbOrg?.ImageBaseUrl ?? "" }
            });
        }

        public async Task<IEnumerable<ShowMovieDbIdModel>> GetAllShowIdMovieDbIdAsync()
        {
            return await QueryAsync<ShowMovieDbIdModel>(@"
                SELECT Id, MovieDbId FROM site.Show
                WHERE IsDeleted <> TRUE
            ", new Dictionary<string, object>() { });
        }

        public async Task<IEnumerable<SeasonEpisodeResponseEntity>> GetShowSeasonsEpisodesByShowIdAsync(int showId)
        {
            return await QueryAsync<SeasonEpisodeResponseEntity>(@"
                SELECT
                    SH.Id as ShowId,
                    SE.Id as SeasonId,
                    EP.Id as EpisodeId,
                    MAX(SE.Name) as SeasonName,
                    MAX(EP.EpisodeNumber) as EpisodeNumber,
                    MAX(EP.Name) as EpisodeName,
                    CASE 
                        WHEN (SELECT UE.EpisodeId FROM site.UserEpisode UE WHERE UE.EpisodeId = EP.Id AND UE.IsDeleted <> TRUE LIMIT 1) IS NULL THEN FALSE
                        ELSE TRUE
                    END as IsWatched
                FROM
                    site.Show SH,
                    site.Season SE,
                    site.Episode EP
                WHERE
                    SH.Id = SE.ShowId
                    AND SE.Id = EP.SeasonId
                    AND SH.Id = @ShowId
                GROUP BY SH.Id, SE.Id, EP.Id
                ORDER BY SH.Id, SE.Id, EP.Id ASC
            ", new Dictionary<string, object>()
            {
                { "ShowId", showId }
            });
        }
    }
}
