using System;

namespace Nager.Immich.Models
{
    public class SharedLinkResponseDto
    {
        public string Id { get; set; }
        public object Description { get; set; }
        public object Password { get; set; }
        public string UserId { get; set; }
        public string Key { get; set; }
        public string Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ExpiresAt { get; set; }
        //public Asset[] assets { get; set; }
        public bool AllowUpload { get; set; }
        public bool AllowDownload { get; set; }
        public bool ShowMetadata { get; set; }
        public string Slug { get; set; }
    }
}
