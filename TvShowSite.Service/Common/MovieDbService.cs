using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TvShowSite.Core.Helpers;
using TvShowSite.Domain.ApiEntities;
using TvShowSite.Domain.ApiEntities.MovieDbOrg;
using TvShowSite.Domain.Entities;

namespace TvShowSite.Service.Common
{
    public class MovieDbService
    {
        private readonly HttpHelper _httpHelper;

        public MovieDbService(HttpHelper httpHelper)
        {
            _httpHelper = httpHelper;
        }

        public async Task<MovieDbShowSearchResponse?> GetShowSearchResultsAsync(string name, int page = 1)
        {
            var encodedString = HttpUtility.UrlDecode(name);

            var parameters = new Dictionary<string, string>
            {
                { "query", encodedString },
                { "page", page.ToString() }
            };

            var requestString = PrepareUrl("/search/tv", parameters, null);

            var response = await GetResultsFromAPIAsync<MovieDbShowSearchResponse>(requestString);

            return response;
        }

        public async Task<ShowDetail?> GetShowDetailsAsync(int movieDbId)
        {
            var parameters = new Dictionary<string, string>
            {
                { "tv", movieDbId.ToString() }
            };

            var requestString = PrepareUrl(null, null, parameters);

            var response = await GetResultsFromAPIAsync<ShowDetail>(requestString);

            return response;
        }

        public async Task<SeasonDetail?> GetSeasonDetailsAsync(int tvShowMovieDbId, int seasonNumber)
        {
            var parameters = new Dictionary<string, string>
            {
                { "tv", tvShowMovieDbId.ToString() },
                { "season", seasonNumber.ToString() }
            };

            var requestString = PrepareUrl(null, null, parameters);

            var response = await GetResultsFromAPIAsync<SeasonDetail>(requestString);

            return response;
        }

        public async Task<EpisodeDetail?> GetEpisodeDetailsAsync(int tvShowMovieDbId, int seasonNumber, int episodeNumber)
        {
            var parameters = new Dictionary<string, string>
            {
                { "tv", tvShowMovieDbId.ToString() },
                { "season", seasonNumber.ToString() },
                { "episode", episodeNumber.ToString() },
            };

            var requestString = PrepareUrl(null, null, parameters);

            var response = await GetResultsFromAPIAsync<EpisodeDetail>(requestString);

            return response;
        }

        public async Task<CreditDetail?> GetCreditDetailsAsync(int tvShowMovieDbId, int seasonNumber, int episodeNumber)
        {
            var parameters = new Dictionary<string, string>
            {
                { "tv", tvShowMovieDbId.ToString() },
                { "season", seasonNumber.ToString() },
                { "episode", episodeNumber.ToString() },
                { "credits", string.Empty }
            };

            var requestString = PrepareUrl(null, null, parameters).TrimEnd('/');

            var response = await GetResultsFromAPIAsync<CreditDetail>(requestString);

            return response;
        }

        private static string PrepareUrl(string? basePath, Dictionary<string, string>? queryParameters, Dictionary<string, string>? pathParameters)
        {
            var sb = new StringBuilder(SettingsHelper.Settings?.ApiDetails?.TheMovieDbOrg?.BaseUrl);

            if (!string.IsNullOrEmpty(basePath))
            {
                sb.Append(basePath);
            }

            if (pathParameters?.Any() == true)
            {
                foreach(var keyValuePair in pathParameters)
                {
                    sb.Append($"/{keyValuePair.Key}/{keyValuePair.Value}");
                }
            }

            if(queryParameters?.Any() == true)
            {
                for(int i = 0; i < queryParameters.Count; i++)
                {
                    sb.Append((i == 0 ? "?" : "&") + queryParameters.ElementAt(i).Key + "=" + queryParameters.ElementAt(i).Value);
                }
            }

            return sb.ToString();
        }

        private async Task<T?> GetResultsFromAPIAsync<T>(string url)
        {
            if(!string.IsNullOrEmpty(url))
            {
                var headers = new Dictionary<string, string>()
                {
                    { "Authorization", "Bearer " + SettingsHelper.Settings?.ApiDetails?.TheMovieDbOrg?.AccessToken },
                };

                var result = await _httpHelper.GetAsync<T>(url, headers);

                return result ?? default;
            }
            else
            {
                return default;
            }
        }
    }
}
