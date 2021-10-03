using FluentAssertions;
using System;
using System.Collections.Generic;
using WeatherCheck.Domain.Models;
using Xunit;

namespace UnitTest
{
    public class WeatherTest
    {
        [Theory]
        [InlineData(300)]
        [InlineData(400)]
        [InlineData(100)]
        public void Should_GoOut(int weatherCode)
        {
            //arrange
            Weather sut = Weather.Create(weatherCode, 1, 1, "", default);
            var rainingWeatherCodes = new HashSet<int>() { 176, 179, 182 };

            //act
            var result = sut.ShouldGoOutside(rainingWeatherCodes);

            //assert
            result.Should().BeTrue("It's not raining");
        }

        [Theory]
        [InlineData(176)]
        [InlineData(179)]
        [InlineData(182)]
        public void ShouldNot_GoOut(int weatherCode)
        {
            //arrange
            Weather sut = Weather.Create(weatherCode, 1, 1, "", default);
            var rainingWeatherCodes = new HashSet<int>() { 176, 179, 182 };

            //act
            var result = sut.ShouldGoOutside(rainingWeatherCodes);

            //assert
            result.Should().BeFalse($"Weather Code is {weatherCode}");
        }

        [Theory]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(20)]
        public void Should_ApplySunscreen(int uvIndex)
        {
            //arrange
            Weather sut = Weather.Create(311, 1, uvIndex, "", default);

            //act
            var result = sut.ShouldApplySunscreen();

            //assert
            result.Should().BeTrue($"UV Index is {uvIndex}");
        }

        [Theory]
        [InlineData(3)]
        [InlineData(2)]
        [InlineData(1)]
        public void ShouldNot_ApplySunscreen(int uvIndex)
        {
            //arrange
            Weather sut = Weather.Create(311, 1, uvIndex, "", default);

            //act
            var result = sut.ShouldApplySunscreen();

            //assert
            result.Should().BeFalse($"UV Index is {uvIndex}");
        }

        [Theory]
        [InlineData(113, 20)]
        [InlineData(116, 31)]
        [InlineData(119, 100)]
        [InlineData(122, 30)]
        [InlineData(143, 25)]
        public void Should_FlyAKite(int weatherCode, int windSpeed)
        {
            //arrange
            Weather sut = Weather.Create(weatherCode, windSpeed, 0, "", default);
            var rainingWeatherCodes = new HashSet<int>() { 176, 179, 182 };

            //act
            var result = sut.CanFlyAKite(rainingWeatherCodes);

            //assert
            result.Should().BeTrue($"Weather Code is {weatherCode} and Wind Speed is {windSpeed}");
        }


        [Theory]
        [InlineData(176, 5)]
        [InlineData(143, 5)]
        [InlineData(176, 16)]
        public void ShouldNot_FlyAKite(int weatherCode, int windSpeed)
        {
            //arrange
            Weather sut = Weather.Create(weatherCode, windSpeed, 0, "", default);
            var rainingWeatherCodes = new HashSet<int>() { 176, 179, 182 };

            //act
            var result = sut.CanFlyAKite(rainingWeatherCodes);

            //assert
            result.Should().BeFalse($"Weather Code is {weatherCode} and Wind Speed is {windSpeed}");
        }
    }
}
