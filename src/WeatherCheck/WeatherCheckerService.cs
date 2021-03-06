using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WeatherCheck.Application.Features.Queries.CheckCurrentWeather;
using WeatherCheck.Application.SeedWork.Exceptions;

namespace WeatherCheck
{
    public class WeatherCheckerService : IWeatherCheckerService
    {
        private const string INITIAL_MESSAGE = "Hi there! Tell me your Zip Code and I can check the weather for you.";
        private const string FOLLOW_UP_MESSAGE = "Do you want to check the weather again?";
        private readonly IMediator _mediator;
        private readonly ILogger<WeatherCheckerService> _logger;
        private bool _isInitialPrompt = true;
        private bool _isBusy = false;

        public WeatherCheckerService(IMediator mediator, ILogger<WeatherCheckerService> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task CheckCurrentWeatherAsync()
        {
            while (true)
            {
                string zipCode = string.Empty;
                try
                {
                    Console.WriteLine(_isInitialPrompt ? INITIAL_MESSAGE : FOLLOW_UP_MESSAGE);
                    _isInitialPrompt = false;
                    Console.WriteLine("Press \"Ctrl + C\" to exit.");
                    Console.Write("Zip Code: ");
                    zipCode = Console.ReadLine() ?? string.Empty;

                    StartSpinner();
                    var result = await _mediator.Send(new CheckCurrentWeatherQuery(zipCode));
                    StopSpinner();
                    var outPutSentenceBuilder = new SentenceBuilder(result);
                    Console.WriteLine();
                    Console.WriteLine(outPutSentenceBuilder.Build());
                    Console.WriteLine("Prease any key to check again or \"Ctrl + C\" to exit.");
                    Console.ReadKey();
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
                    Console.WriteLine("Sorry, there was an unknown error.");
                    Console.WriteLine("Please try again.");
                    _logger.LogError(ex, "There was an exception trying to check the weather with Zip Code: {zipCode}", zipCode);
                }
                finally
                {
                    StopSpinner();
                }
            }
        }

        public void StartSpinner()
        {
            Task.Run(async () =>
            {
                _isBusy = true;
                Console.Write("Checking");
                while (_isBusy)
                {
                    Console.Write(".");
                    await Task.Delay(500);
                }
            });

        }

        public void StopSpinner()
        {
            _isBusy = false;
        }
    }
}
