using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherCheck.Application.Features.Queries.CheckCurrentWeather
{
    public class CheckCurrentWeatherValidator : AbstractValidator<CheckCurrentWeatherQuery>
    {
        public CheckCurrentWeatherValidator()
        {
            RuleFor(v => v.ZipCode).NotEmpty().Must(zipCode => zipCode.All(char.IsDigit));
        }
    }
}
