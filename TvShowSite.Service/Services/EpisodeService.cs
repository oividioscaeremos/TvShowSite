using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShowSite.Data.Repositories.BagRepositories;
using TvShowSite.Domain.Entities.ShowEntities;
using TvShowSite.Domain.TableEntities.BagEntities;
using TvShowSite.Service.ValidationServices;

namespace TvShowSite.Service.Services
{
    public class EpisodeService
    {
        private readonly UserEpisodeRepository _userEpisodeRepository;
        private readonly HomeService _homeService;

        public EpisodeService(
            UserEpisodeRepository userEpisodeRepository,
            HomeService homeService)
        {
            _userEpisodeRepository = userEpisodeRepository;
            _homeService = homeService;
        }

        public async Task<MarkAsWatchedResponse> MarkAsWatchedAsync(MarkAsWatchedRequest request, int userId)
        {
            var response = new MarkAsWatchedResponse()
            {
                ErrorList = ShowValidationService.ValidateMarkAsWatchedRequest(request)
            };

            if (response.Status)
            {
                var userEpisodeEntity = new UserEpisode
                {
                    EpisodeId = request.EpisodeId!.Value,
                    UserId = userId
                };

                await _userEpisodeRepository.InsertAsync(userEpisodeEntity, userId);

                var userNextToWatch = await _homeService.GetUserNextToWatchAsync(userId);

                if (userNextToWatch.Value?.Any(userShow => userShow.ShowId == request.ShowId!.Value) == true)
                {
                    var nextToWatch = userNextToWatch.Value.First(userShow => userShow.ShowId == request.ShowId!.Value);

                    response.Value = new MarkAsWatchedResponseEntity
                    {
                        EpisodeId = nextToWatch.EpisodeId,
                        EpisodeNumber = nextToWatch.EpisodeNumber,
                        SeasonNumber = nextToWatch.SeasonNumber,
                    };
                }
                else
                {
                    response.Value = new MarkAsWatchedResponseEntity
                    {
                        IsFinished = true
                    };
                }
            }

            return response;
        }
    }
}
