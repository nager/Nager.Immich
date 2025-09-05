namespace ImmichClient
{
    public class Assets
    {
        public int Total { get; set; }
        public int Count { get; set; }
        public Item[] Items { get; set; }
        public object[] Facets { get; set; }
        public object NextPage { get; set; }
    }
}
