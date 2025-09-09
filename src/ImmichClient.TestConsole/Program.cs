using ImmichClient;

var immichUrl = new Uri("");
var apiKey = "";

var httpClient = new HttpClient();
var immichClient = new ImmichApiClient(httpClient, apiKey, $"{immichUrl.AbsoluteUri}api/");

var albumResponses = await immichClient.GetAlbumsAsync();
foreach (var albumResponse in albumResponses)
{
    var albumInfoResponse = await immichClient.GetAlbumAsync(albumResponse.Id);
    var assets = albumInfoResponse.Assets;
}

Console.WriteLine(albumResponses.Length);