using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Nager.Immich.Models;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Nager.Immich
{
    public class ImmichClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ImmichClient> _logger;

        public ImmichClient(
            HttpClient httpClient,
            string apiKey,
            string baseAddress,
            ILogger<ImmichClient>? logger = default)
        {
            this._httpClient = httpClient;
            this._httpClient.BaseAddress = new Uri(baseAddress);
            this._logger = logger ?? new NullLogger<ImmichClient>();

            this._httpClient.DefaultRequestHeaders.TryAddWithoutValidation("x-api-key", apiKey);
        }

        public async Task<AlbumResponseDto[]?> GetAlbumsAsync(
            CancellationToken cancellationToken = default)
        {
            using var responseMessage = await this._httpClient.GetAsync("albums", cancellationToken);
            if (!responseMessage.IsSuccessStatusCode)
            {
                this._logger.LogError($"{nameof(GetAlbumsAsync)} - Cannot get albums, statusCode:{responseMessage.StatusCode}");

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
                this._logger.LogError($"{nameof(GetAlbumAsync)} - Cannot get album, statusCode:{responseMessage.StatusCode}");

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
            CancellationToken cancellationToken = default)
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
