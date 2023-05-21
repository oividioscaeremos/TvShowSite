using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShowSite.Data.Repositories.BagRepositories;
using TvShowSite.Domain.Entities.ShowEntities;

namespace TvShowSite.Service.Services
{
    public class HomeService
    {
        private readonly UserEpisodeRepository _userEpisodeRepository;

        public HomeService(
            UserEpisodeRepository userEpisodeRepository)
        {
            _userEpisodeRepository = userEpisodeRepository;
        }

        public async Task<GetUserNextToWatchResponse> GetUserNextToWatchAsync(int userId)
        {
            var response = new GetUserNextToWatchResponse()
            {
                Value = new List<UserShowHomeEntity>()
            };
            var userShows = await _userEpisodeRepository.GetUserNextToWatchAsync(userId);

            var userLastWatchedShows = await _userEpisodeRepository.GetUserLastWatchedShowIdsAsync(userId);

            if (userShows is not null && userLastWatchedShows is not null)
            {
                userLastWatchedShows = userLastWatchedShows.Where(showId => userShows.Any(userShow => userShow.ShowId == showId));

                response.Value = userLastWatchedShows.Select(showId => userShows.First(userShow => userShow.ShowId == showId)).ToList();

                if (response.Value.Count != userShows.Count())
                {
                    response.Value = response.Value.Concat(userShows.Where(userShow => !response.Value.Any(r => r.ShowId == userShow.ShowId))).ToList();
                }
            }
            else if (userShows is not null)
            {
                response.Value = userShows.ToList();
            }

            return response;
        }
    }
}
