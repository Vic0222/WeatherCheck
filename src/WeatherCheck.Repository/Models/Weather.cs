using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace WeatherCheck.WeatherStackClient.Models
{
    public class Weather
    {
        [JsonPropertyName("observation_time")]
        public string ObservationTime { get; set; }

        [JsonPropertyName("temperature")]
        public long Temperature { get; set; }

        [JsonPropertyName("weather_code")]
        public int WeatherCode { get; set; }

        [JsonPropertyName("weather_icons")]
        public Uri[] WeatherIcons { get; set; }

        [JsonPropertyName("weather_descriptions")]
        public string[] WeatherDescriptions { get; set; }

        [JsonPropertyName("wind_speed")]
        public long WindSpeed { get; set; }

        [JsonPropertyName("wind_degree")]
        public long WindDegree { get; set; }

        [JsonPropertyName("wind_dir")]
        public string WindDir { get; set; }

        [JsonPropertyName("pressure")]
        public long Pressure { get; set; }

        [JsonPropertyName("precip")]
        public long Precip { get; set; }

        [JsonPropertyName("humidity")]
        public long Humidity { get; set; }

        [JsonPropertyName("cloudcover")]
        public long Cloudcover { get; set; }

        [JsonPropertyName("feelslike")]
        public long Feelslike { get; set; }

        [JsonPropertyName("uv_index")]
        public long UvIndex { get; set; }

        [JsonPropertyName("visibility")]
        public long Visibility { get; set; }

        [JsonPropertyName("is_day")]
        public string IsDay { get; set; }
    }
}