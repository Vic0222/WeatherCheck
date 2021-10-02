using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using WeatherCheck;
using WeatherCheck.Application.Features.Queries.CheckCurrentWeather;
using WeatherCheck.Application.Options;
using WeatherCheck.Application.SeedWork.Repositories;
using WeatherCheck.WeatherStackClient.Repositories;

using IHost host = Startup.CreateHostBuilder(args).Build();
var weatherCheckerService = host.Services.GetRequiredService<WeatherCheckerService>();
await weatherCheckerService.CheckCurrentWeatherAsync();

