using ImmichClient.Models;
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

        public async Task<AlbumResponseDto[]?> GetAlbumsAsync(
            CancellationToken cancellationToken = default)
        {
            using var responseMessage = await this._httpClient.GetAsync("albums", cancellationToken);
            if (!responseMessage.IsSuccessStatusCode)
            {
                return null;
            }

            return await responseMessage.Content.ReadFromJsonAsync<AlbumResponseDto[]>(cancellationToken);
        }

        public async Task<AlbumResponseDto?> GetAlbumAsync(
            string albumId,
            CancellationToken cancellationToken = default)
        {
            using var responseMessage = await this._httpClient.GetAsync($"albums/{albumId}", cancellationToken);
            if (!responseMessage.IsSuccessStatusCode)
            {
                return null;
            }

            return await responseMessage.Content.ReadFromJsonAsync<AlbumResponseDto>(cancellationToken);
        }

        public async Task<bool> CreateAlbumAsync(
            CreateAlbumDto createRequest,
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

        public async Task<AlbumsAddAssetsResponseDto?> AddAssetsToAlbumAsync(
            AlbumsAddAssetsDto albumsAddAssets,
            CancellationToken cancellationToken = default)
        {
            using var responseMessage = await this._httpClient.PutAsJsonAsync("albums/assets", albumsAddAssets, cancellationToken);
            if (!responseMessage.IsSuccessStatusCode)
            {
                return null;
            }

            return await responseMessage.Content.ReadFromJsonAsync<AlbumsAddAssetsResponseDto>(cancellationToken);
        }

        public async Task<BulkIdResponseDto[]?> RemoveAssetFromAlbumAsync(
            string albumId,
            BulkIdsDto bulkIds,
            CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"albums/{albumId}/assets", UriKind.Relative),
                Content = JsonContent.Create(bulkIds)
            };

            using var responseMessage = await this._httpClient.SendAsync(request, cancellationToken);
            if (!responseMessage.IsSuccessStatusCode)
            {
                return null;
            }

            return await responseMessage.Content.ReadFromJsonAsync<BulkIdResponseDto[]>(cancellationToken);
        }

        public async Task<PeopleResponseDto?> GetPeoplesAsync(
            CancellationToken cancellationToken = default)
        {
            using var responseMessage = await this._httpClient.GetAsync("people", cancellationToken);
            if (!responseMessage.IsSuccessStatusCode)
            {
                return null;
            }

            return await responseMessage.Content.ReadFromJsonAsync<PeopleResponseDto>(cancellationToken);
        }

        public async Task<SearchResponseDto?> GetAssetsAsync(
            MetadataSearchDto metadataSearch,
            CancellationToken cancellationToken= default)
        {
            using var responseMessage = await this._httpClient.PostAsJsonAsync("search/metadata", metadataSearch, cancellationToken);
            if (!responseMessage.IsSuccessStatusCode)
            {
                return null;
            }

            return await responseMessage.Content.ReadFromJsonAsync<SearchResponseDto>(cancellationToken);
        }

        public async Task<SharedLinkResponseDto?> CreateSharedLinkAsync(
            SharedLinkCreateDto sharedLinkCreate,
            CancellationToken cancellationToken = default)
        {
            using var responseMessage = await this._httpClient.PostAsJsonAsync("shared-links", sharedLinkCreate, cancellationToken);
            if (!responseMessage.IsSuccessStatusCode)
            {
                return null;
            }

            return await responseMessage.Content.ReadFromJsonAsync<SharedLinkResponseDto>(cancellationToken);
        }
    }
}
