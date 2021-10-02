using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WeatherCheck.Application.Options
{
    public class WeatherTypes
    {
        public List<int> RainingWeatherCodes { get; set; }

        public WeatherTypes()
        {
            RainingWeatherCodes = new List<int>();
        }
    }
}
