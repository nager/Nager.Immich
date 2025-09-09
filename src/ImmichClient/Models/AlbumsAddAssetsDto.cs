namespace ImmichClient.Models
{
    public class AlbumsAddAssetsDto
    {
        public required string[] AlbumIds { get; set; }
        public required string[] AssetIds { get; set; }
    }
}
