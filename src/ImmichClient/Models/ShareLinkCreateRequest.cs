namespace ImmichClient
{
    public class ShareLinkCreateRequest
    {
        public bool AllowDownload {  get; set; }
        public bool AllowUpload { get; set; } = false;
        public string Type { get; set; }
        public string[] AssetIds { get; set; } = [];
        public DateTime? ExpiresAt { get; set; }
        public string Description { get; set; }
        public string Slug { get; set; }
    }
}
