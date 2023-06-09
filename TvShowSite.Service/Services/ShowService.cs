﻿using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShowSite.Core.ExtensionMethods;
using TvShowSite.Core.Helpers;
using TvShowSite.Data.Repositories;
using TvShowSite.Data.Repositories.BagRepositories;
using TvShowSite.Domain.ApiEntities.MovieDbOrg;
using TvShowSite.Domain.Entities.ShowEntities;
using TvShowSite.Domain.TableEntities;
using TvShowSite.Domain.TableEntities.BagEntities;
using TvShowSite.Service.Common;
using TvShowSite.Service.ValidationServices;

namespace TvShowSite.Service.Services
{
    public class ShowService
    {
        private readonly CountryRepository _countryRepository;
        private readonly CharacterRepository _characterRepository;
        private readonly CharacterEpisodeRepository _characterEpisodeRepository;
        private readonly EpisodeRepository _episodeRepository;
        private readonly LanguageRepository _languageRepository;
        private readonly ProductionCompanyRepository _productionCompanyRepository;
        private readonly SeasonRepository _seasonRepository;
        private readonly ShowRepository _showRepository;
        private readonly UserEpisodeRepository _userEpisodeRepository;
        private readonly UserEpisodeNoteRepository _userEpisodeNoteRepository;
        private readonly UserShowRepository _userShowRepository;
        private readonly MovieDbService _movieDbService;

        public ShowService(
            CountryRepository countryRepository,
            CharacterRepository characterRepository,
            CharacterEpisodeRepository characterEpisodeRepository,
            EpisodeRepository episodeRepository,
            LanguageRepository languageRepository,
            ProductionCompanyRepository productionCompanyRepository,
            SeasonRepository seasonRepository,
            ShowRepository showRepository,
            UserEpisodeRepository userEpisodeRepository,
            UserEpisodeNoteRepository userEpisodeNoteRepository,
            UserShowRepository userShowRepository,
            MovieDbService movieDbService)
        {
            _countryRepository = countryRepository;
            _characterRepository = characterRepository;
            _characterEpisodeRepository = characterEpisodeRepository;
            _episodeRepository = episodeRepository;
            _languageRepository = languageRepository;
            _productionCompanyRepository = productionCompanyRepository;
            _seasonRepository = seasonRepository;
            _showRepository = showRepository;
            _userEpisodeRepository = userEpisodeRepository;
            _userEpisodeNoteRepository = userEpisodeNoteRepository;
            _userShowRepository = userShowRepository;
            _movieDbService = movieDbService;
        }

        public async Task<ShowSearchResponse> SearchAsync(ShowSearchRequest request, int userId)
        {
            var response = new ShowSearchResponse();

            if(request.Name is not null)
            {
                var showsFromAPI = await _movieDbService.GetShowSearchResultsAsync(request.Name, request.Page ?? 1);

                if (showsFromAPI?.Results is not null && showsFromAPI.Results.Count() > 0)
                {
                    response.TotalResults = showsFromAPI.TotalResults;
                    response.TotalPages = showsFromAPI.TotalPages;

                    var userShowIds = await _userShowRepository.GetUserShowMovieDbIdsAsync(userId);
                    var showIdMovieDbIds = await _showRepository.GetAllShowIdMovieDbIdAsync();

                    response.Value = showsFromAPI.Results.Select(result => new ShowSearchResponseEntity
                    {
                        Id = showIdMovieDbIds?.FirstOrDefault(model => model.MovieDbId == result.Id!)?.Id,
                        MovieDbId = result.Id,
                        ShowName = result.Name,
                        PosterURL = SettingsHelper.Settings?.ApiDetails?.TheMovieDbOrg?.ImageBaseUrl + result.PosterPath,
                        IsAdded = userShowIds.Contains(result.Id!.Value)
                    }).ToList();
                }
                else
                {
                    response.ErrorList.Add($"Could not find any shows with the name '{request.Name}'");
                }
            }

            return response;
        }

        public async Task<AddShowResponse> AddShowAsync(AddShowRequest request, int userId)
        {
            var response = new AddShowResponse
            {
                ErrorList = ShowValidationService.ValidateAddShowRequest(request)
            };

            if(response.Status)
            {
                if(request.Id.HasValue)
                {
                    var show = await _showRepository.GetByIdAsync(request.Id.Value);

                    if(show is not null)
                    {
                        var dbUserShowEntity = new UserShow
                        {
                            ShowId = show.Id,
                            UserId = userId
                        };

                        await _userShowRepository.InsertAsync(dbUserShowEntity, userId);
                    }
                    else
                    {
                        response.ErrorList.Add("The show could not be found on the system.");
                    }
                }
                else if(request.TheMovieDbId.HasValue)
                {
                    int showId;
                    var show = await _showRepository.GetShowByMovieDbOrgIdAsync(request.TheMovieDbId.Value);

                    if(show is not null)
                    {
                        showId = show.Id;
                    }
                    else
                    {
                        response.ErrorList = await this.AddShowAsync(request.TheMovieDbId.Value, userId);

                        var addedShow = await _showRepository.GetShowByMovieDbOrgIdAsync(request.TheMovieDbId.Value);

                        showId = addedShow.Id;

                        await this.HandleMultipleCharactersAsync(showId);
                    }

                    if (response.Status && request.AddToShows)
                    {
                        var dbUserShow = await _userShowRepository.GetUserShowsByShowIdAsync(userId, showId);

                        if(dbUserShow is null)
                        {
                            var dbUserShowEntity = new UserShow
                            {
                                ShowId = showId,
                                UserId = userId
                            };


                            await _userShowRepository.InsertAsync(dbUserShowEntity, userId);
                        }
                    }

                    response.Value = showId;
                }
            }

            return response;
        }

        public async Task<RemoveShowResponse> RemoveShowAsync(RemoveShowRequest request, int userId)
        {
            var response = new RemoveShowResponse
            {
                ErrorList = ShowValidationService.ValidateRemoveShowRequest(request)
            };

            if(response.Status)
            {
                var show = await _showRepository.GetByIdAsync(request.Id!.Value);

                if(show is not null)
                {
                    await _userShowRepository.RemoveFromUserShowsAsync(userId, request.Id.Value);
                }
                else
                {
                    response.ErrorList.Add("Show to be removed could not be found.");
                }
            }

            return response;
        }

        public async Task<ShowDescriptionSearchResponse> GetShowDescriptionAsync(int? id)
        {
            var response = new ShowDescriptionSearchResponse();

            if (id.HasValue)
            {
                response.Value = await _showRepository.GetShowDescriptionAsync(id.Value);
            }
            else
            {
                response.ErrorList.Add("Cannot find show description without identification number.");
            }

            return response;
        }

        public async Task<ShowNameResponse> GetShowNameAsync(int? id)
        {
            var response = new ShowNameResponse();

            if(id.HasValue)
            {
                response.Value = await _showRepository.GetShowNameAsync(id.Value);
            }
            else
            {
                response.ErrorList.Add("Cannot find show description without identification number.");
            }

            return response;
        }

        public async Task<GetPosterURLResponse> GetShowPosterURLAsync(int? id)
        {
            var response = new GetPosterURLResponse();

            if (id.HasValue)
            {
                response.Value = await _showRepository.GetShowPosterURLAsync(id.Value);
            }
            else
            {
                response.ErrorList.Add("Cannot find show description without identification number.");
            }

            return response;
        }

        public async Task<FavoriteCharactersResponse> GetShowFavoriteCharactersAsync(int? id, int? episodeId, int userId)
        {
            var response = new FavoriteCharactersResponse()
            {
                Value = new List<FavoriteCharactersResponsEntity>()
            };

            if (!id.HasValue) response.ErrorList.Add("Show identifier cannot be empty.");

            if (response.Status)
            {
                response.Value = (await _characterEpisodeRepository.GetCharactersByShowIdAndEpisodeIdAsync(id!.Value, episodeId, userId)).ToList();
            }

            return response;
        }

        public async Task<SeasonEpisodeResponse> GetShowSeasonsEpisodesAsync(int? showId)
        {
            var response = new SeasonEpisodeResponse()
            {
                Value = new List<SeasonEpisodeResponseEntity>()
            };

            if (!showId.HasValue) response.ErrorList.Add("Show identifier cannot be empty.");

            if (response.Status)
            {
                response.Value = (await _showRepository.GetShowSeasonsEpisodesByShowIdAsync(showId!.Value)).ToList();
            }

            return response;
        }

        public async Task<UserShowStatusResponse> GetUserShowStatusAsync(int? showId, int userId)
        {
            var response = new UserShowStatusResponse();

            if (!showId.HasValue) response.ErrorList.Add("Show identifier cannot be empty.");

            if(response.Status)
            {
                response.Value = (await _userShowRepository.GetUserShowsByUserIdAsync(userId)).Any(s => s.ShowId == showId!.Value);
            }

            return response;
        }

        public async Task<GetShowNotesResponse> GetShowNotesAsync(int? showId, int userId)
        {
            var response = new GetShowNotesResponse();

            var showNotes = await _userEpisodeNoteRepository.GetShowNotesAsync(showId, userId);

            if(showNotes?.Any() == true)
            {
                response.Value = showNotes
                    .GroupBy(entity => entity.ShowId)
                    .Select(entity => new GetShowNotesShowEntity
                    {
                        ShowId = entity.Key,
                        ShowName = entity.First().ShowName,
                        Notes = entity.Select(details => new GetShowNotesNoteEntity
                        {
                            EpisodeId = details.EpisodeId,
                            NoteContent = details.NoteContent,
                            SeasonNumber = details.SeasonNumber,
                            EpisodeNumber = details.EpisodeNumber
                        }).ToList()
                    }).ToList();
            }

            return response;
        }

        public async Task<GetUserShowResponse> GetUserShowsAsync(int? userId, int loggedInUserId)
        {
            var response = new GetUserShowResponse();

            var userShows = await _userShowRepository.GetUserProfileShowsByUserIdAsync(userId ?? loggedInUserId);

            if (userShows?.Any() == true)
            {
                response.Value = userShows.ToList();
            }

            return response;
        }

        private async Task<List<string>> AddShowAsync(int movieDbId, int userId)
        {
            var errorList = new List<string>();

            var dbShow = await _showRepository.GetShowByMovieDbOrgIdAsync(movieDbId);

            var showDetails = await _movieDbService.GetShowDetailsAsync(movieDbId);

            if (dbShow is null)
            {
                if (showDetails is not null)
                {
                    var show = new Show
                    {
                        Name = showDetails.Name,
                        Description = showDetails.Description,
                        OriginalName = showDetails.OriginalName,
                        PosterURL = showDetails.PosterURL,
                        FirstAirDate = showDetails.FirstAirDate,
                        LastAirDate = showDetails.LastAirDate,
                        IsOngoing = showDetails.IsOnGoing ?? true,
                        EpisodeRunTime = showDetails.EpisodeRuntime != null ? showDetails.EpisodeRuntime[0] : -1,
                        Popularity = Convert.ToInt32(showDetails.Popularity),
                        MovieDbId = showDetails.Id,
                    };

                    if (showDetails.OriginCountries?.Any() == true)
                    {
                        var country = showDetails.OriginCountries.First();

                        var dbCountry = await _countryRepository.GetByNameAsync(country);

                        if (dbCountry is not null)
                        {
                            show.CountryId = dbCountry.Id;
                        }
                        else
                        {
                            var dbCountryToInsert = new Country
                            {
                                Name = country
                            };

                            await _countryRepository.InsertAsync(dbCountryToInsert, userId);

                            show.CountryId = dbCountryToInsert.Id;
                        }
                    }

                    if (!string.IsNullOrEmpty(showDetails.OriginalLanguage))
                    {
                        var dbLanguage = await _languageRepository.GetByNameAsync(showDetails.OriginalLanguage!);

                        if (dbLanguage is not null)
                        {
                            show.OriginalLanguageId = dbLanguage.Id;
                        }
                        else
                        {
                            var dbLanguageToInsert = new Language
                            {
                                Name = showDetails.OriginalLanguage
                            };

                            await _languageRepository.InsertAsync(dbLanguageToInsert, userId);

                            show.OriginalLanguageId = dbLanguageToInsert.Id;
                        }
                    }

                    if (showDetails.ProductionCompanies?.Any() == true)
                    {
                        var productionCompany = showDetails.ProductionCompanies.First();

                        var dbProductionCompany = await _productionCompanyRepository.GetByNameAsync(productionCompany.Name!);

                        if (dbProductionCompany is not null)
                        {
                            show.ProductionCompanyId = dbProductionCompany.Id;
                        }
                        else
                        {
                            var dbProductionCompanyToInsert = new ProductionCompany
                            {
                                Name = productionCompany.Name
                            };

                            await _productionCompanyRepository.InsertAsync(dbProductionCompanyToInsert, userId);

                            show.ProductionCompanyId = dbProductionCompanyToInsert.Id;
                        }
                    }

                    await _showRepository.InsertAsync(show, userId);

                    if (showDetails.Seasons?.Any() == true)
                    {
                        await Parallel.ForEachAsync(showDetails.Seasons, new ParallelOptions { MaxDegreeOfParallelism = 3 }, async (season, cancellationToken) =>
                        {
                            await this.AddSeasonAsync(show.MovieDbId.Value, show.Id, season.SeasonNumber, userId, errorList);
                        });
                    }
                }
                else
                {
                    errorList.Add("Show details could not be fetched.");
                }
            }
            else
            {
                if(showDetails is not null)
                {
                    if (showDetails.Seasons?.Any() == true)
                    {
                        await Parallel.ForEachAsync(showDetails.Seasons, new ParallelOptions { MaxDegreeOfParallelism = 3 }, async (season, cancellationToken) =>
                        {
                            await this.AddSeasonAsync(dbShow.MovieDbId!.Value, dbShow.Id, season.SeasonNumber, userId, errorList);
                        });
                    }
                }
                else
                {
                    errorList.Add("Show details could not be fetched.");
                }
            }
            return errorList;
        }

        private async Task AddSeasonAsync(int showMovieDbId, int showId, int seasonNumber, int userId, List<string> errorList)
        {
            var seasonDetailsFromApi = await _movieDbService.GetSeasonDetailsAsync(showMovieDbId, seasonNumber);

            if (seasonDetailsFromApi is not null)
            {
                int dbSeasonId;

                var dbSeason = await _seasonRepository.GetByMovieDbIdAsync(seasonDetailsFromApi.Id);

                if(dbSeason is not null)
                {
                    dbSeasonId = dbSeason.Id;
                }
                else
                {
                    var dbSeasonToInsert = new Season
                    {
                        MovieDbId = seasonDetailsFromApi.Id,
                        SeasonNumber = seasonNumber,
                        ShowId = showId,
                        Name = seasonDetailsFromApi.Name,
                        Description = seasonDetailsFromApi.Description,
                        PosterURL = seasonDetailsFromApi.PosterURL,
                        AirDate = seasonDetailsFromApi.AirDate
                    };

                    await _seasonRepository.InsertAsync(dbSeasonToInsert, userId);

                    dbSeasonId = dbSeasonToInsert.Id;
                }

                if(seasonDetailsFromApi.Episodes?.Any() == true)
                {
                    await Parallel.ForEachAsync(seasonDetailsFromApi.Episodes, new ParallelOptions { MaxDegreeOfParallelism = 3 }, async (episode, cancellationToken) =>
                    {
                        await this.AddEpisodeAsync(showMovieDbId, showId, dbSeasonId, seasonNumber, episode.EpisodeNumber, userId, errorList);
                    });
                }
            }
        }

        private async Task AddEpisodeAsync(int showMovieDbId, int showId, int seasonId, int seasonNumber, int episodeNumber, int userId, List<string> errorList)
        {
            var episodeDetailsFromApi = await _movieDbService.GetEpisodeDetailsAsync(showMovieDbId, seasonNumber, episodeNumber);
            
            if(episodeDetailsFromApi is not null)
            {
                int dbEpisodeId;
                var dbEpisode = await _episodeRepository.GetByMovieDbIdAsync(episodeDetailsFromApi.Id);

                if(dbEpisode is not null)
                {
                    dbEpisodeId = dbEpisode.Id;
                }
                else
                {
                    var dbEpisodeToInsert = new Episode
                    {
                        MoviedbId = episodeDetailsFromApi.Id,
                        ShowId = showId,
                        SeasonId = seasonId,
                        EpisodeNumber = episodeNumber,
                        Name = episodeDetailsFromApi.Name,
                        Description = episodeDetailsFromApi.Description,
                        AirDate = episodeDetailsFromApi.AirDate,
                        PosterURL = episodeDetailsFromApi.PosterURL
                    };

                    await _episodeRepository.InsertAsync(dbEpisodeToInsert, userId);

                    dbEpisodeId = dbEpisodeToInsert.Id;
                }

                var episodeCreditDetails = await _movieDbService.GetCreditDetailsAsync(showMovieDbId, seasonNumber, episodeNumber);

                if(episodeCreditDetails?.Cast?.Any() == true)
                {
                    if(episodeCreditDetails.GuestStars?.Any() == true)
                    {
                        episodeCreditDetails.Cast = episodeCreditDetails.Cast.Concat(episodeCreditDetails.GuestStars).ToList();
                    }

                    await Parallel.ForEachAsync(episodeCreditDetails.Cast, new ParallelOptions { MaxDegreeOfParallelism = 3 }, async (cast, cancellationToken) =>
                    {
                        int characterId;

                        var systemCharacter = await _characterRepository.GetCharacterFromMovideDbIdAsync(cast.Id);

                        if (systemCharacter is null)
                        {
                            var dbCharacterToInsert = new Character
                            {
                                CharacterName = cast.CharacterName,
                                CharacterOrder = cast.Order,
                                MovieDbId = cast.Id,
                                PosterURL = cast.PosterURL,
                                Role = cast.Department,
                                ShowId = showId
                            };

                            await _characterRepository.InsertAsync(dbCharacterToInsert, userId);

                            characterId = dbCharacterToInsert.Id;
                        }
                        else
                        {
                            characterId = systemCharacter.Id;
                        }

                        var dbCharacterEpisode = await _characterEpisodeRepository.GetByCharacterIdSeasonIdEpisodeIdAsync(characterId, seasonId, dbEpisodeId);

                        if (dbCharacterEpisode is null)
                        {
                            var characterEpisodeEntity = new CharacterEpisode
                            {
                                CharacterId = characterId,
                                EpisodeId = dbEpisodeId,
                                SeasonId = seasonId,
                                VoteCount = 0
                            };

                            await _characterEpisodeRepository.InsertAsync(characterEpisodeEntity, userId);
                        }
                    });
                }
            }
        }

        private async Task HandleMultipleCharactersAsync(int showId)
        {
            var showCharacters = await _characterRepository.GetCharactersByShowIdAsync(showId);

            if (showCharacters?.Any() == true)
            {
                var groupedCharacters = showCharacters.GroupBy(character => character.MovieDbId).Where(g => g.Count() > 1);

                foreach (var characterGroup in groupedCharacters)
                {
                    var characterToRetain = characterGroup.First();

                    var deleteCharacters = characterGroup.Where(c => c.Id != characterToRetain.Id);

                    foreach (var character in deleteCharacters)
                    {
                        await _characterEpisodeRepository.UpdateCharacterIdByCharacterIdAsync(character.Id, characterToRetain.Id);

                        await _characterEpisodeRepository.HardDeleteByCharacterIdAsync(character.Id);

                        await _characterRepository.HardDeleteCharacterByIdAsync(character.Id);
                    }
                }
            }
        }
    }
}
