using Newtonsoft.Json;
using System.Net;

namespace TvShowSite.Core.Helpers
{
    public class HttpHelper
    {
        private readonly static HttpClient _httpClient = new();

        private readonly LogHelper _logHelper;

        public HttpHelper(LogHelper logHelper)
        {
            _logHelper = logHelper;
        }

        public async Task<T?> GetAsync<T>(string url, Dictionary<string, string>? headers = null)
        {
            var response = await GetAsync(url, headers);

            if(response is not null)
            {
                using(var sr = new StreamReader(response))
                {
                    var textResponse = await sr.ReadToEndAsync();

                    try
                    {
                        var deserializedResponse = JsonConvert.DeserializeObject<T>(textResponse);

                        return deserializedResponse;
                    }
                    catch (Exception ex)
                    {
                        _logHelper.LogException("GetAsync<T> deserialization threw an exception.", "Core", "HttpHelper", "GetAsync<T>", ex, new { url, headers, textResponse });
                    }
                }
            }

            return default;
        }

        public async Task<Stream?> GetAsync(string url, Dictionary<string, string>? headers = null)
        {
            string guid = Guid.NewGuid().ToString();

            try
            {
                var httpRequestMessage = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(url)
                };

                if (headers is not null)
                {
                    foreach (var key in headers.Keys)
                    {
                        httpRequestMessage.Headers.Add(key, headers[key]);
                    }
                }

                var response = await _httpClient.SendAsync(httpRequestMessage);
                
                if(response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStreamAsync();
                }
                else
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    _logHelper.LogInformation("Http Get failed.", "Core", "HttpHelper", "GetAsync", new { guid, responseContent });

                    throw new ApplicationException(responseContent);
                }    
            }
            catch (Exception ex)
            {
                _logHelper.LogException("Http Get resulted with an exception.", "Core", "HttpHelper", "GetAsync", ex, new { guid, url, headers });

                return null;
            }
        }
    }
}
