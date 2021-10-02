using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherCheck.Domain.SeedWork;

namespace WeatherCheck.Domain.Models
{
    public class Weather 
    {


        public int WeatherCode { get; private set; }

        public string WeatherDescription { get; private set; }

        public long WindSpeed { get; private set; }

        public long UvIndex { get; private set; }

        private Weather(int weatherCode, long windSpeed, long uvIndex, string weatherDescription)
        {
            WeatherCode = weatherCode;
            WindSpeed = windSpeed;
            UvIndex = uvIndex;
            WeatherDescription = weatherDescription;
        }

        /// <summary>
        /// Check if it's raining.
        /// </summary>
        private bool isRaining(IEnumerable<int> rainingWeatherCodes)
        {
            return rainingWeatherCodes.Contains(WeatherCode);
        }

        /// <summary>
        /// Checks if you should go outside
        /// </summary>
        /// <param name="rainingWeatherCodes">A list of weather codes considered to be raining</param>
        /// <returns>Returns true if <paramref name="rainingWeatherCodes"/> contains <see cref="Weather.WeatherCode"/> </returns>
        public bool ShouldGoOutside(IEnumerable<int> rainingWeatherCodes)
        {
            return !isRaining(rainingWeatherCodes);
        }

        /// <summary>
        /// Checks if you should wear a sunscreen
        /// </summary>
        /// <returns>True if <see cref="UvIndex"/> is greater that 3.</returns>
        public bool ShouldApplySunscreen()
        {
            return UvIndex > 3;
        }

        /// <summary>
        /// Checks if you can fly a kite.
        /// </summary>
        /// <param name="rainingWeatherCodes"></param>
        /// <returns>True if <see cref="isRaining(IEnumerable{int})"/> is false and <see cref="WindSpeed"/> is over 15</returns>
        public bool CanFlyAKite(IEnumerable<int> rainingWeatherCodes)
        {
            return !isRaining(rainingWeatherCodes) && WindSpeed > 15;
        }

        public static Weather Create(int weatherCode, long windSpeed, long uvIndex, string weatherDescription)
        {
            if (weatherCode <= 0)
            {
                throw new DomainException($"Invalid {nameof(weatherCode)}");
            }
            if (windSpeed <= 0)
            {
                throw new DomainException($"Invalid {nameof(windSpeed)}");
            }
            if (uvIndex < 0)
            {
                throw new DomainException($"Invalid {nameof(uvIndex)}");
            }
            return new Weather(weatherCode, windSpeed, uvIndex, weatherDescription);
        }

        
    }
}
