namespace Nager.Immich.Models
{
    public class AssetFaceWithoutPersonResponseDto
    {
        public string Id { get; set; }
        public int ImageHeight { get; set; }
        public int ImageWidth { get; set; }
        public int BoundingBoxX1 { get; set; }
        public int BoundingBoxX2 { get; set; }
        public int BoundingBoxY1 { get; set; }
        public int BoundingBoxY2 { get; set; }

        /// <summary>
        /// SourceType (machine-learning, exif, manual)
        /// </summary>
        public string SourceType { get; set; }
    }
}
