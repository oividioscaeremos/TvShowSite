using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShowSite.Data.Repositories;
using TvShowSite.Data.Repositories.BagRepositories;
using TvShowSite.Domain.Common;
using TvShowSite.Domain.Entities.EpisodeEntities;
using TvShowSite.Domain.TableEntities.BagEntities;
using TvShowSite.Service.ValidationServices;

namespace TvShowSite.Service.Services
{
    public class EpisodeService
    {
        private readonly EpisodeRepository _episodeRepository;
        private readonly UserEpisodeRepository _userEpisodeRepository;
        private readonly UserEpisodeNoteRepository _userEpisodeNoteRepository;
        private readonly UserCharacterVoteRepository _userCharacterVoteRepository;
        private readonly HomeService _homeService;

        public EpisodeService(
            EpisodeRepository episodeRepository,
            UserEpisodeRepository userEpisodeRepository,
            UserEpisodeNoteRepository userEpisodeNoteRepository,
            UserCharacterVoteRepository userCharacterVoteRepository,
            HomeService homeService)
        {
            _episodeRepository = episodeRepository;
            _userEpisodeRepository = userEpisodeRepository;
            _userEpisodeNoteRepository = userEpisodeNoteRepository;
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
                        SeasonId = nextToWatch.SeasonId,
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

        public async Task<MarkAsNotWatchedResponse> MarkAsNotWatchedAsync(MarkAsNotWatchedRequest request, int userId)
        {
            var response = new MarkAsNotWatchedResponse()
            {
                ErrorList = ShowValidationService.ValidateMarkAsWatchedRequest(request)
            };

            if (response.Status)
            {
                await _userEpisodeRepository.MarkAsDeletedByUserIdAndEpisodeIdAsync(request.EpisodeId!.Value, userId);

                var userNextToWatch = await _homeService.GetUserNextToWatchAsync(userId);

                if (userNextToWatch.Value?.Any(userShow => userShow.ShowId == request.ShowId!.Value) == true)
                {
                    var nextToWatch = userNextToWatch.Value.First(userShow => userShow.ShowId == request.ShowId!.Value);

                    response.Value = new MarkAsWatchedResponseEntity
                    {
                        EpisodeId = nextToWatch.EpisodeId,
                        SeasonId = nextToWatch.SeasonId,
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
                        SeasonId = userShowHome.SeasonId,
                        EpisodeId = userShowHome.EpisodeId
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
                await _userCharacterVoteRepository.MarkVotesAsDeletedByUserIdAndEpisodeIdAsync(userId, request.EpisodeId!.Value);

                var newVote = new UserCharacterVote
                {
                    CharacterId = request.CharacterId!.Value,
                    EpisodeId = request.EpisodeId.Value,
                    UserId = userId
                };

                await _userCharacterVoteRepository.InsertAsync(newVote, userId);
            }

            return response;
        }

        public async Task<RemoveVoteResponse> RemoveVoteAsync(RemoveVoteRequest request, int userId)
        {
            var response = new RemoveVoteResponse()
            {
                ErrorList = EpisodeValidationService.ValidateRemoveVoteRequest(request)
            };

            if (response.Status)
            {
                await _userCharacterVoteRepository.MarkVotesAsDeletedByUserIdAndEpisodeIdAsync(userId, request.EpisodeId!.Value);
            }

            return response;
        }

        public async Task<BaseResponse<string>> GetEpisodeDescriptionAsync(int? episodeId)
        {
            var response = new BaseResponse<string>();

            if (!episodeId.HasValue) response.ErrorList.Add("Episode identifier cannot be empty.");

            if (response.Status)
            {
                response.Value = await _episodeRepository.GetEpisodeDescriptionAsync(episodeId!.Value);
            }

            return response;
        }

        public async Task<BaseResponse<string>> GetEpisodeNameAsync(int? episodeId)
        {
            var response = new BaseResponse<string>();

            if (!episodeId.HasValue) response.ErrorList.Add("Episode identifier cannot be empty.");

            if (response.Status)
            {
                response.Value = await _episodeRepository.GetEpisodeNameAsync(episodeId!.Value);
            }

            return response;
        }

        public async Task<BaseResponse<bool>> GetEpisodeWatchedStatusAsync(int? episodeId, int userId)
        {
            var response = new BaseResponse<bool>();

            if (!episodeId.HasValue) response.ErrorList.Add("Episode identifier cannot be empty.");

            if (response.Status)
            {
                response.Value = await _episodeRepository.GetEpisodeWatchedStatusAsync(episodeId!.Value, userId);
            }

            return response;
        }

        public async Task<AddEpisodeNoteResponse> AddEpisodeNoteAsync(AddEpisodeNoteRequest request, int userId)
        {
            var response = new AddEpisodeNoteResponse()
            {
                ErrorList = EpisodeValidationService.ValidateAddEpisodeNoteRequest(request)
            };

            if (response.Status)
            {
                var userEpisodeNote = new UserEpisodeNote()
                {
                    UserId = userId,
                    EpisodeId = request.EpisodeId!.Value,
                    Content = request.Content
                };

                await _userEpisodeNoteRepository.InsertAsync(userEpisodeNote, userId);

                response.Value = new AddEpisodeNoteResponseEntity()
                {
                    Id = userId,
                    UserId = userId,
                    EpisodeId = request.EpisodeId!.Value,
                    Content = request.Content
                };
            }

            return response;
        }

        public async Task<GetEpisodeNotesResponse> GetEpisodeNotesAsync(int? episodeId, int userId)
        {
            var response = new GetEpisodeNotesResponse();

            if (!episodeId.HasValue) response.ErrorList.Add("Episode identifier cannot be empty.");

            if (response.Status)
            {
                var episodeNote = await _userEpisodeNoteRepository.GetAllNotesByEpisodeIdAsync(episodeId!.Value, userId);

                if (episodeNote?.Any() == true)
                {
                    response.Value = episodeNote.Select(entity => new GetEpisodeNotesResponseEntity
                    {
                        Id = entity.Id,
                        Content = entity.Content,
                        EpisodeId = entity.EpisodeId,
                        UserId = entity.UserId
                    }).ToList();
                }
            }

            return response;
        }

        public async Task<UpdateEpisodeNoteResponse> UpdateEpisodeNoteAsync(UpdateEpisodeNoteRequest request, int userId)
        {
            var response = new UpdateEpisodeNoteResponse()
            {
                ErrorList = EpisodeValidationService.ValidateUpdateEpisodeNoteRequest(request)
            };

            if (response.Status)
            {
                await _userEpisodeNoteRepository.UpdateNoteContentByIdAsync(request.NoteId!.Value, request.NewContent!, userId);
            }

            return response;
        }

        public async Task<DeleteEpisodeNoteResponse> DeleteEpisodeNoteAsync(int? noteId, int userId)
        {
            var response = new DeleteEpisodeNoteResponse();

            if (!noteId.HasValue) response.ErrorList.Add("Note identifier cannot be empty.");

            if (response.Status)
            {
                var note = await _userEpisodeNoteRepository.GetByIdAsync(noteId!.Value);

                await _userEpisodeNoteRepository.MarkAsDeletedAsync(note, userId);
            }

            return response;
        }
    }
}
