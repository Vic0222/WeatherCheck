using System.Threading.Tasks;

namespace WeatherCheck
{
    public interface IWeatherCheckerService
    {
        Task CheckCurrentWeatherAsync();
    }
}