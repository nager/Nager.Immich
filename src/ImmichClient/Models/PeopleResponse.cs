namespace ImmichClient
{
    public class PeopleResponse
    {
        public Person[] People { get; set; }
        public bool HasNextPage { get; set; }
        public int Total { get; set; }
        public int Hidden { get; set; }
    }
}
