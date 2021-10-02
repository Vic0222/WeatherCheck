using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WeatherCheck.Application.SeedWork.Repositories;
using WeatherCheck.Domain.Models;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using WeatherCheck.Application.SeedWork.Exceptions;
using Microsoft.Extensions.Options;
using WeatherCheck.Application.Options;

namespace WeatherCheck.WeatherStackClient.Repositories
{
    public class WeatherRepository : IWeatherRepository
    {
        private readonly ILogger<WeatherRepository> _logger;
        private readonly WeatherStackConfig _weatherStackConfig;
        private readonly HttpClient _httpClient;

        public WeatherRepository(ILogger<WeatherRepository> logger, IOptions<WeatherStackConfig> weatherStackConfig, HttpClient httpClient)
        {
            _logger = logger;
            _weatherStackConfig = weatherStackConfig.Value;
            _httpClient = httpClient;
        }

        public async Task<Weather> GetCurrentWeatherAsync(string zipCode, CancellationToken cancellationToken)
        {
            try
            {
                var weatherCheckResponse = await _httpClient.GetFromJsonAsync<Models.WeatherCheckResponse>($"/current?access_key={_weatherStackConfig.ApiKey}&query={zipCode}", cancellationToken);
                if (weatherCheckResponse?.Error != null)
                {
                    _logger.LogError("There was an error retreiving the current weather. {error}", weatherCheckResponse?.Error);

                    throw new PersistenceException(weatherCheckResponse?.Error?.Code.ToString() ?? string.Empty, weatherCheckResponse?.Error?.Info ?? string.Empty);

                }
                return Weather.Create(weatherCheckResponse?.Current?.WeatherCode ?? 0, weatherCheckResponse?.Current?.WindSpeed ?? 0, weatherCheckResponse?.Current?.UvIndex ?? 0, weatherCheckResponse?.Current?.WeatherDescriptions?.FirstOrDefault() ?? string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unknown error on weather stack api.");
                throw;
            }
            
        }
    }
}
