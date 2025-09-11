using System;

namespace Nager.Immich.Models
{
    public class AssetMediaCreateDto
    {
        public required string DeviceAssetId { get; set; }
        public required string DeviceId { get; set; }
        //public string? Duration { get; set; }
        public required DateTime FileCreatedAt { get; set; }
        public required DateTime FileModifiedAt { get; set; }
        public string? Filename { get; set; }
        public bool IsFavorite { get; set; } = false;
        //public Guid? LivePhotoVideoId { get; set; }
        //public List<AssetMetadataUpsertItemDto> Metadata { get; set; } = new();
        //public Stream? SidecarData { get; set; }
        //public AssetVisibility? Visibility { get; set; }
    }
}
