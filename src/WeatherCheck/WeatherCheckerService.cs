using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WeatherCheck.Application.Features.Queries.CheckCurrentWeather;
using WeatherCheck.Application.SeedWork.Exceptions;

namespace WeatherCheck
{
    public class WeatherCheckerService
    {
        private const string INITIAL_MESSAGE = "Hi there! Tell me your Zip Code and I can check the weather for you.";
        private const string FOLLOW_UP_MESSAGE = "Do you want to check the weather again?";
        private readonly IMediator _mediator;
        private readonly ILogger<WeatherCheckerService> _logger;
        private int _promtCount = 0;

        public WeatherCheckerService(IMediator mediator, ILogger<WeatherCheckerService> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task CheckCurrentWeatherAsync()
        {
            while (true)
            {
                string zipCode= string.Empty;
                try
                {
                    Console.WriteLine(_promtCount == 0 ? INITIAL_MESSAGE : FOLLOW_UP_MESSAGE);
                    Console.Write("Zip Code: ");
                    zipCode = Console.ReadLine() ?? string.Empty;
                    _promtCount++;

                    await _mediator.Send(new CheckCurrentWeatherQuery(zipCode));

                }
                catch (InvalidZipCodeException ex)
                {
                    Console.WriteLine($"Sorry, you provided an invalid Zip Code({ex.ZipCode}).");
                    Console.WriteLine("Please check and try again.");
                }
                catch (PersistenceException)
                {
                    Console.WriteLine("Sorry, there was an unexpected error trying to check the current weather.");
                    Console.WriteLine("Please try again.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "There was an exception trying to check the weather with Zip Code: {zipCode}", zipCode);
                }
            }
        }
    }
}
