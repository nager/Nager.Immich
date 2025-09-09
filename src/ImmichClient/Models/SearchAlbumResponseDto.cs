using ImmichClient.Models;

namespace ImmichClient
{
    public class SearchAlbumResponseDto
    {
        public int Total { get; set; }
        public int Count { get; set; }
        public AlbumResponseDto[] Items { get; set; }
        public SearchFacetResponseDto[] Facets { get; set; }
    }
}
