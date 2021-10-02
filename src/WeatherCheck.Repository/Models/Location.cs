#nullable disable

using System.Text.Json.Serialization;

namespace WeatherCheck.WeatherStackClient.Models
{
    public class Location
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("region")]
        public string Region { get; set; }

        [JsonPropertyName("lat")]
        public string Lat { get; set; }

        [JsonPropertyName("lon")]
        public string Lon { get; set; }

        [JsonPropertyName("timezone_id")]
        public string TimezoneId { get; set; }

        [JsonPropertyName("localtime")]
        public string Localtime { get; set; }

        [JsonPropertyName("localtime_epoch")]
        public long LocaltimeEpoch { get; set; }

        [JsonPropertyName("utc_offset")]
        public string UtcOffset { get; set; }
    }
}