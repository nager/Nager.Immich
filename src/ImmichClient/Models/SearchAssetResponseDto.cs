namespace ImmichClient.Models
{
    public class SearchAssetResponseDto
    {
        public int Total { get; set; }
        public int Count { get; set; }
        public AssetResponseDto[] Items { get; set; }
        public SearchFacetResponseDto[] Facets { get; set; }
        public object NextPage { get; set; }
    }
}
