using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WeatherCheck.Domain.Models;

namespace WeatherCheck.Application.SeedWork.Repositories
{
    public interface IWeatherRepository
    {
        Task<Weather> GetCurrentWeatherAsync(string zipCode, CancellationToken cancellationToken = default);
    }
}
