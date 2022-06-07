using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace FunTrophy.Web.Authentication
{
    public class CustomAuthorizationHandler: DelegatingHandler
    {
        private readonly ILocalStorageService _localStorageService;

        public CustomAuthorizationHandler(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var jwtToken = await _localStorageService.GetItemAsync<string>("authToken");

            if (!string.IsNullOrWhiteSpace(jwtToken))
                request.Headers.Authorization = new AuthenticationHeaderValue("bearer", jwtToken);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
