using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherCheck.Domain.Models
{
    public record Location(string Name, string Region, string Country)
    {
        public string GetAddress()
        {
            return $"{Name}, {Region}, {Country}";
        }
    }
}
