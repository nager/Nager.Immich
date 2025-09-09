namespace ImmichClient.Models
{
    public class CreateAlbumDto
    {
        public string AlbumName { get; set; }
        public string[] AssetIds { get; set; } = [];
    }
}
