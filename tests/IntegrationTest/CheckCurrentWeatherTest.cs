using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherCheck.Application.Features.Queries.CheckCurrentWeather;
using WeatherCheck.Application.Options;
using WeatherCheck.Application.SeedWork.Repositories;
using WeatherCheck.Domain.Models;
using WeatherCheck.Shared.Dto;
using Xunit;

namespace IntegrationTest
{
    public class CheckCurrentWeatherTest
    {
        [Theory]
        [InlineData(100, 16, 4)]
        [InlineData(99, 20, 6)]
        [InlineData(300, 16, 9)]
        public async Task Should_GoOutside_ApplySunscreen_FlyKite(int weatherCode, int windSpeed, int uvIndex)
        {
            //arrange
            string zipCode = "40170";
            var query = new CheckCurrentWeatherQuery(zipCode);

            //setup fake repo
            Location location = new("Name", "Country", "Region");
            var weather = Weather.Create(weatherCode, windSpeed, uvIndex, "Unknown", location);

            var fakeWeatherRepository = A.Fake<IWeatherRepository>();
            A.CallTo(() => fakeWeatherRepository.GetCurrentWeatherAsync(zipCode, default)).Returns(Task.FromResult(weather));

            //setup weather codes
            WeatherTypes weatherTypes = new()
            {
                RainingWeatherCodes = new HashSet<int>()
                {
                    176, 179, 182
                }
            };
            var weatherTypesOption = Options.Create(weatherTypes);

            //validator
            var validator = new CheckCurrentWeatherValidator();
            var sut = new CheckCurrentWeatherHandler(fakeWeatherRepository, validator, weatherTypesOption);

            //act
            CheckCurrentWeatherResultDto result = await sut.Handle(query, default);

            //assert
            A.CallTo(() => fakeWeatherRepository.GetCurrentWeatherAsync(zipCode, default)).MustHaveHappened();
            result.ShouldGoOut.Should().BeTrue();
            result.ShouldApplySunscreen.Should().BeTrue();
            result.CanFlyKite.Should().BeTrue();
        }
    }
}
