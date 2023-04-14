using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TvShowSite.Core.Helpers;
using TvShowSite.Domain.ApiEntities;
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

            var requestString = PrepareUrl(parameters);

            var response = await GetResultsFromAPIAsync<MovieDbShowSearchResponse>(requestString);

            return response;
        }

        private static string PrepareUrl(Dictionary<string, string> parameters)
        {
            if(parameters is not null)
            {
                var sb = new StringBuilder(SettingsHelper.Settings?.ApiDetails?.TheMovieDbOrg?.BaseUrl);

                for(int i = 0; i < parameters.Count; i++)
                {
                    sb.Append((i == 0 ? "?" : "&") + parameters.ElementAt(i).Key + "=" + parameters.ElementAt(i).Value);
                }

                return sb.ToString();
            }
            else
            {
                return SettingsHelper.Settings?.ApiDetails?.TheMovieDbOrg?.BaseUrl ?? string.Empty;
            }
        }

        private async Task<T?> GetResultsFromAPIAsync<T>(string url)
        {
            if(string.IsNullOrEmpty(url))
            {
                var headers = new Dictionary<string, string>()
                {
                    { "Content-Type", "application/json" },
                    { "Accept", "application/json" },
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
