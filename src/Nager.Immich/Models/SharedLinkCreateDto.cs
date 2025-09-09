using System;

namespace Nager.Immich.Models
{
    public class SharedLinkCreateDto
    {
        public bool AllowDownload {  get; set; }
        public bool AllowUpload { get; set; } = false;

        /// <summary>
        /// SharedLinkType (ALBUM or INDIVIDUAL)
        /// </summary>
        public string Type { get; set; }
        public string[] AssetIds { get; set; } = [];
        public DateTime? ExpiresAt { get; set; }
        public string Description { get; set; }
        public string Slug { get; set; }
    }
}
