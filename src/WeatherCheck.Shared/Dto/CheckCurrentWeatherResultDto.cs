using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherCheck.Shared.Dto
{
    public record CheckCurrentWeatherResultDto(bool ShouldGoOut, bool ShouldApplySunscreen, bool CanFlyKite, string WeatherDescription)
    {
    }
}
