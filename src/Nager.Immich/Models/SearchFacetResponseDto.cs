namespace Nager.Immich.Models
{
    public class SearchFacetResponseDto
    {
        public SearchFacetCountResponseDto[] Counts { get; set; }
        public string FieldName { get; set; }
    }
}
