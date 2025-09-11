using System.Text.Json.Serialization;

namespace Nager.Immich.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AssetMediaStatus
    {
        Created,
        Replaced,
        Duplicate
    }
}
