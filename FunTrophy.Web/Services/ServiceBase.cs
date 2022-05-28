using System.Net.Http.Json;
using System.Text;
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

        protected string GetUrl(string parameterName, object parameterValue)
        {
            var parameters = new Dictionary<string, object>
            {
                { parameterName, parameterValue }
            };
            return GetUrl(parameters);
        }

        protected string GetUrl(Dictionary<string, object> queryParameters)
        {
            if (queryParameters == null || !queryParameters.Any())
            {
                return _basePath;
            }

            var url = _basePath + "?";
            var parameters = string.Join("&", queryParameters.Select(p => $"{p.Key}={p.Value.ToString()}"));
            url += parameters;
            return url;
        }

        protected string GetUrl()
        {
            return _basePath;
        }

        protected async Task<T> GetAsync<T>(string url) where T : class
        {
            var response = await _httpClient.GetFromJsonAsync<T>(url);
            if (response == null)
                throw new KeyNotFoundException();
            return response;
        }

        private async Task<HttpResponseMessage> BasePostAsync(string url, object? body)
        {
            var json = JsonSerializer.Serialize(body);
            var content = body is null ? null : new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content);

            if (response == null)
                throw new KeyNotFoundException();

            return response;
        }

        protected async Task PostAsync(string url, object? body)
        {
            await BasePostAsync(url, body);
        }

        protected async Task<T> PostAsync<T>(string url, object? body)
        {
            var response = await BasePostAsync(url, body);
            var responseJson = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<T>(responseJson);

            if (result == null)
                throw new NullReferenceException();

            return result;
        }

        protected async Task DeleteAsync(string url)
        {
            var response = await _httpClient.DeleteAsync(url);
            if (response == null)
                throw new KeyNotFoundException();
        }

        protected async Task UpdateAsync(string url, object body)
        {
            var response = await _httpClient.PutAsJsonAsync(url, body);

            if (response == null)
                throw new KeyNotFoundException();
        }
    }
}