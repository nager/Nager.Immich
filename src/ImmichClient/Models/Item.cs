namespace ImmichClient
{
    public class Item
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string DeviceAssetId { get; set; }
        public string OwnerId { get; set; }
        public string DeviceId { get; set; }
        public object LibraryId { get; set; }
        public string Type { get; set; }
        public string OriginalPath { get; set; }
        public string OriginalFileName { get; set; }
        public string OriginalMimeType { get; set; }
        public string Thumbhash { get; set; }
        public DateTime FileCreatedAt { get; set; }
        public DateTime FileModifiedAt { get; set; }
        public DateTime LocalDateTime { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsArchived { get; set; }
        public bool IsTrashed { get; set; }
        public string Visibility { get; set; }
        public string Duration { get; set; }
        public object LivePhotoVideoId { get; set; }
        public Person[] People { get; set; }
        public Face[] UnassignedFaces { get; set; }
        public string Checksum { get; set; }
        public bool IsOffline { get; set; }
        public bool HasMetadata { get; set; }
        public object DuplicateId { get; set; }
        public bool Resized { get; set; }
    }
}
