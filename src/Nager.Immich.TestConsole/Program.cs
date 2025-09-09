using Nager.Immich;
using Nager.Immich.Models;

var immichUrl = new Uri("");
var apiKey = "";

var httpClient = new HttpClient();
var immichClient = new ImmichClient(httpClient, apiKey, $"{immichUrl.AbsoluteUri}api/");

var albumResponses = await immichClient.GetAlbumsAsync();
foreach (var albumResponse in albumResponses)
{
    var albumInfoResponse = await immichClient.GetAlbumAsync(albumResponse.Id);
}
