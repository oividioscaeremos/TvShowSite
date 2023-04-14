using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShowSite.Data.Repositories;
using TvShowSite.Domain.Entities;
using TvShowSite.Service.Common;

namespace TvShowSite.Service.Services
{
    public class ShowService
    {
        private readonly ShowRepository _showRepository;
        private readonly MovieDbService _movieDbService;

        public ShowService(
            ShowRepository showRepository,
            MovieDbService movieDbService)
        {
            _showRepository = showRepository;
            _movieDbService = movieDbService;
        }

        public async Task<ShowSearchResponse> SearchAsync(ShowSearchRequest request)
        {
            var response = new ShowSearchResponse();

            if(request.Name is not null)
            {
                var shows = await _showRepository.GetShowsByNameAsync(request.Name.Trim(), request.Page, request.PageSize);

                var showsFromAPI = await _movieDbService.GetShowSearchResultsAsync(request.Name, request.Page ?? 1);

                if(showsFromAPI?.Results is not null && showsFromAPI.Results.Count() > shows.Count())
                {
                    response.TotalResults = showsFromAPI.TotalResults;
                    response.TotalPages = showsFromAPI.TotalPages;

                    response.Value = showsFromAPI.Results.Select(result => new ShowSearchResponseEntity
                    {
                        ShowName = result.Name,
                        PosterURL = result.PosterPath
                    }).ToList();
                }
                else if(shows.Any())
                {
                    response.TotalResults = shows.Count();
                    response.Value = shows.Select(dbShow =>
                    {

                        return new ShowSearchResponseEntity
                        {
                            EpisodeCount = 1
                        };
                    }).ToList();
                }
                else
                {
                    response.ErrorList.Add($"Could not find any shows with the name '{request.Name}'");
                }
            }

            return response;
        }
    }
}
