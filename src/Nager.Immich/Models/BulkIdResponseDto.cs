namespace Nager.Immich.Models
{
    public class BulkIdResponseDto
    {
        public required string Id { get; set; }
        public bool Success { get; set; }
        public string? Error { get; set; }
    }
}
