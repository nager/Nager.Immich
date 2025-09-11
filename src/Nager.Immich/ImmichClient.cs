using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Nager.Immich.Helpers;
using Nager.Immich.Models;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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
            using var request = new HttpRequestMessage
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

        public async Task<AssetMediaResponseDto?> UploadAssetAsync(
            string filePath,
            CancellationToken cancellationToken = default)
        {
            //TODO: AssetMediaCreateDto

            var fileInfo = new FileInfo(filePath);
            if (!fileInfo.Exists)
            {
                return null;
            }

            using var content = new MultipartFormDataContent
            {
                { new StringContent($"{filePath}-{fileInfo.LastWriteTimeUtc.Ticks}"), "deviceAssetId" },
                { new StringContent("csharp"), "deviceId" },
                { new StringContent(fileInfo.CreationTimeUtc.ToString("o")), "fileCreatedAt" },
                { new StringContent(fileInfo.LastWriteTimeUtc.ToString("o")), "fileModifiedAt" },
                { new StringContent("false"), "isFavorite" }
            };

            var checksum = FileHelper.ComputeSha1Checksum(filePath);
            if (!string.IsNullOrEmpty(checksum))
            {
                content.Headers.Add("x-immich-checksum", checksum);
            }

            var fileBytes = await File.ReadAllBytesAsync(filePath, cancellationToken);

            using var fileContent = new ByteArrayContent(fileBytes);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            content.Add(fileContent, "assetData", Path.GetFileName(filePath));

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("assets?slug=test", UriKind.Relative),
                Content = content
            };

            using var responseMessage = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            if (!responseMessage.IsSuccessStatusCode)
            {
                var error = await responseMessage.Content.ReadAsStringAsync();

                this._logger.LogError($"{nameof(UploadAssetAsync)} - {error}");

                return null;
            }

            return await responseMessage.Content.ReadFromJsonAsync<AssetMediaResponseDto>(cancellationToken);
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
