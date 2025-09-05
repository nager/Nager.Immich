﻿namespace ImmichClient
{
    public class Album
    {
        public string AlbumName { get; set; }
        public string Description { get; set; }
        public string AlbumThumbnailAssetId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Id { get; set; }
        public string OwnerId { get; set; }
        public Owner Owner { get; set; }
        public object[] AlbumUsers { get; set; }
        public bool Shared { get; set; }
        public bool HasSharedLink { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public object[] Assets { get; set; }
        public int AssetCount { get; set; }
        public bool IsActivityEnabled { get; set; }
        public string Order { get; set; }
        public DateTime LastModifiedAssetTimestamp { get; set; }
    }
}
