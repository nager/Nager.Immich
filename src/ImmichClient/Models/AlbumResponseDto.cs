namespace ImmichClient.Models
{
    public class AlbumResponseDto
    {
        public string AlbumName { get; set; }
        public string AlbumThumbnailAssetId { get; set; }
        public object[] AlbumUsers { get; set; }
        public int AssetCount { get; set; }
        public AssetResponseDto[] Assets { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Id { get; set; }
        public string OwnerId { get; set; }
        public UserResponseDto Owner { get; set; }
        public bool Shared { get; set; }
        public bool HasSharedLink { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActivityEnabled { get; set; }
        public string Order { get; set; }
        public DateTime LastModifiedAssetTimestamp { get; set; }
    }
}
