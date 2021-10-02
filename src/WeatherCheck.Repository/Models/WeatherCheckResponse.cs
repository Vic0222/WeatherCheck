using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable disable

namespace WeatherCheck.WeatherStackClient.Models
{
    public class WeatherCheckResponse
    {
        public Location Location { get; set; }

        public Weather Current { get; set; }

        public Error Error { get; set; }
    }
}
