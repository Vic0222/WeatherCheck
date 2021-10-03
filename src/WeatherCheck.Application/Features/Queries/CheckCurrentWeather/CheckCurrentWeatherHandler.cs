using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WeatherCheck.Application.Options;
using WeatherCheck.Application.SeedWork.Exceptions;
using WeatherCheck.Application.SeedWork.Repositories;
using WeatherCheck.Shared.Dto;

namespace WeatherCheck.Application.Features.Queries.CheckCurrentWeather
{
    public class CheckCurrentWeatherHandler : IRequestHandler<CheckCurrentWeatherQuery, CheckCurrentWeatherResultDto>
    {
        private readonly IWeatherRepository _weatherRepository;
        private readonly IValidator<CheckCurrentWeatherQuery> _validator;
        private readonly WeatherTypes _weatherTypes;

        public CheckCurrentWeatherHandler(IWeatherRepository weatherRepository, IValidator<CheckCurrentWeatherQuery> validator, IOptions<WeatherTypes> weatherTypesOption)
        {
            _weatherRepository = weatherRepository;
            _validator = validator;
            _weatherTypes = weatherTypesOption.Value;
        }

        public async Task<CheckCurrentWeatherResultDto> Handle(CheckCurrentWeatherQuery request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
            {
                var zipCodeError = validationResult.Errors.FirstOrDefault(e => e.PropertyName == nameof(CheckCurrentWeatherQuery.ZipCode));
                if (zipCodeError != null)
                {
                    throw new InvalidZipCodeException(request.ZipCode, zipCodeError.ErrorMessage);
                }
            }

            string zipCode = request.ZipCode.PadLeft(5, '0');
            var currentWeather = await _weatherRepository.GetCurrentWeatherAsync(zipCode, cancellationToken);

            return new CheckCurrentWeatherResultDto(currentWeather.ShouldGoOutside(_weatherTypes.RainingWeatherCodes), currentWeather.ShouldApplySunscreen(), currentWeather.CanFlyAKite(_weatherTypes.RainingWeatherCodes), currentWeather.WeatherDescription, currentWeather.Location.GetAddress());
        }
    }
}
