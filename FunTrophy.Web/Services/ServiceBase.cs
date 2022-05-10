using System.Net;
using System.Net.Http.Json;
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
            var response = await _httpClient.GetFromJsonAsync<T>(url);
            if (response == null)
                throw new KeyNotFoundException();
            return response;
        }

        protected async Task DeleteAsync(string url)
        {
            var response = await _httpClient.DeleteAsync(url);
            if (response == null)
                throw new KeyNotFoundException();
        }
    }
}
