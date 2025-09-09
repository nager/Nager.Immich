namespace ImmichClient.Models
{
    public class PeopleResponseDto
    {
        public PersonResponseDto[] People { get; set; }
        public bool HasNextPage { get; set; }
        public int Total { get; set; }
        public int Hidden { get; set; }
    }
}
