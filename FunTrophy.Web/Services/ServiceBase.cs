using System.Net;
using System.Text.Json;

namespace FunTrophy.Web.Services
{
    public class ServiceBase
    {
        protected readonly HttpClient _httpClient;
        protected readonly string _basePath;

        public ServiceBase(HttpClient httpClient, string basePath)
        {
            _httpClient = httpClient;
            _basePath = basePath;
        }

        protected string GetUrl(Dictionary<string, string> queryParameters)
        {
            if (queryParameters == null || !queryParameters.Any())
            {
                return _basePath;
            }

            var url = _basePath + "?";
            var parameters = string.Join("&", queryParameters.Select(p => $"{p.Key}={p.Key}"));
            url += parameters;
            return url;
        }

        protected string GetUrl()
        {
            return _basePath;
        }

        protected async Task<T> GetAsync<T>(string url) where T: class
        {
            using var response = await _httpClient.GetAsync(url);
            return await ParseResult<T>(response);

        }

        protected async Task<T> ParseResult<T>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    var result = await JsonSerializer.DeserializeAsync<T>(await response.Content.ReadAsStreamAsync());
                    if (result == null)
                        throw new Exception("Result is empty");
                    return result;
                }
                catch
                {
                    throw new Exception("Error parsing json from result");
                }

            }

            var errorJson = await response.Content.ReadAsStringAsync();
            switch (response.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    throw new KeyNotFoundException(errorJson);
                case HttpStatusCode.BadRequest:
                default:
                    throw new Exception(errorJson);
            }
        }
    }
}
