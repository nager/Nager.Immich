using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Net.Http.Json;

namespace ImmichClient
{
    public class ImmichApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ImmichApiClient> _logger;

        public ImmichApiClient(
            HttpClient httpClient,
            string apiKey,
            string baseAddress,
            ILogger<ImmichApiClient>? logger = default)
        {
            this._httpClient = httpClient;
            this._httpClient.BaseAddress = new Uri(baseAddress);
            this._logger = logger ?? new NullLogger<ImmichApiClient>();

            this._httpClient.DefaultRequestHeaders.TryAddWithoutValidation("x-api-key", apiKey);
        }

        public async Task<Album[]?> GetAlbumAsync(
            CancellationToken cancellationToken = default)
        {
            using var responseMessage = await this._httpClient.GetAsync("albums", cancellationToken);
            if (!responseMessage.IsSuccessStatusCode)
            {
                return null;
            }

            return await responseMessage.Content.ReadFromJsonAsync<Album[]>(cancellationToken);
        }

        public async Task<bool> CreateAlbumAsync(
            AlbumCreateRequest createRequest,
            CancellationToken cancellationToken = default)
        {
            using var responseMessage = await this._httpClient.PostAsJsonAsync("albums", createRequest, cancellationToken);
            return responseMessage.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAlbumAsync(
            string albumId,
            CancellationToken cancellationToken = default)
        {
            using var responseMessage = await this._httpClient.DeleteAsync($"albums/{albumId}", cancellationToken);
            return responseMessage.IsSuccessStatusCode;
        }

        public async Task<PeopleResponse?> GetPeoplesAsync(
            CancellationToken cancellationToken = default)
        {
            using var responseMessage = await this._httpClient.GetAsync("people", cancellationToken);
            if (!responseMessage.IsSuccessStatusCode)
            {
                return null;
            }

            return await responseMessage.Content.ReadFromJsonAsync<PeopleResponse>(cancellationToken);
        }

        public async Task<AssetResponse?> GetAssetsAsync(
            AssetFilterRequest filterRequest,
            CancellationToken cancellationToken= default)
        {
            using var responseMessage = await this._httpClient.PostAsJsonAsync("search/metadata", filterRequest, cancellationToken);
            if (!responseMessage.IsSuccessStatusCode)
            {
                return null;
            }

            return await responseMessage.Content.ReadFromJsonAsync<AssetResponse>(cancellationToken);
        }

        public async Task<SharedLinkResponse?> CreateSharedLinkAsync(
            ShareLinkCreateRequest createRequest,
            CancellationToken cancellationToken = default)
        {
            using var responseMessage = await this._httpClient.PostAsJsonAsync("shared-links", createRequest, cancellationToken);
            if (!responseMessage.IsSuccessStatusCode)
            {
                return null;
            }

            return await responseMessage.Content.ReadFromJsonAsync<SharedLinkResponse>(cancellationToken);
        }
    }
}
