namespace ImmichClient
{
    public class AlbumCreateRequest
    {
        public string AlbumName { get; set; }
        public string[] AssetIds { get; set; } = [];
    }
}
