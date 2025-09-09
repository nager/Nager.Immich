namespace ImmichClient.Models
{
    public class PersonResponseDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public object BirthDate { get; set; }
        public string ThumbnailPath { get; set; }
        public bool IsHidden { get; set; }
        public bool IsFavorite { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
