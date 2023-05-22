using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShowSite.Data.Repositories.BagRepositories;
using TvShowSite.Domain.Entities.EpisodeEntities;
using TvShowSite.Domain.TableEntities.BagEntities;
using TvShowSite.Service.ValidationServices;

namespace TvShowSite.Service.Services
{
    public class EpisodeService
    {
        private readonly UserEpisodeRepository _userEpisodeRepository;
        private readonly UserCharacterVoteRepository _userCharacterVoteRepository;
        private readonly HomeService _homeService;

        public EpisodeService(
            UserEpisodeRepository userEpisodeRepository,
            UserCharacterVoteRepository userCharacterVoteRepository,
            HomeService homeService)
        {
            _userEpisodeRepository = userEpisodeRepository;
            _userCharacterVoteRepository = userCharacterVoteRepository;
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

        public async Task<ShowNextToWatchResponse> GetShowNextToWatchEpisodeAsync(int userId, int? showId)
        {
            var response = new ShowNextToWatchResponse()
            {
                Value = new ShowNextToWatchResponseEntity()
            };

            if (!showId.HasValue) response.ErrorList.Add("Show identifier cannot be empty.");

            if (response.Status)
            {
                var nextToWatchShow = await _userEpisodeRepository.GetUserNextToWatchAsync(userId, showId);

                if (nextToWatchShow?.Any() == true)
                {
                    response.Value = nextToWatchShow.Select(userShowHome => new ShowNextToWatchResponseEntity()
                    {
                        EpisodeNumber = userShowHome.EpisodeNumber,
                        SeasonNumber = userShowHome.SeasonNumber,
                    }).First();
                }
            }

            return response;
        }

        public async Task<AddVoteResponse> AddVoteAsync(AddVoteRequest request, int userId)
        {
            var response = new AddVoteResponse()
            {
                ErrorList = EpisodeValidationService.ValidateAddVoteRequest(request)
            };

            if (response.Status)
            {
                await _userCharacterVoteRepository.MarkOtherVotesAsDeletedForEpisodeAndCharacterByIdAsync(userId, request.EpisodeId!.Value, request.CharacterId!.Value);

                var newVote = new UserCharacterVote
                {
                    CharacterId = request.CharacterId.Value,
                    EpisodeId = request.EpisodeId.Value,
                    UserId = userId
                };

                await _userCharacterVoteRepository.InsertAsync(newVote, userId);
            }

            return response;
        }
    }
}
