using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherCheck.Shared.Dto;

namespace WeatherCheck.Application.Features.Queries.CheckCurrentWeather
{
    public class CheckCurrentWeatherQuery : IRequest<CheckCurrentWeatherResultDto>
    {
        public string ZipCode { get; set; }

        public CheckCurrentWeatherQuery(string zipCode)
        {
            ZipCode = zipCode;
        }
    }
}
