using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Polly;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeatherCheck.Application.Features.Queries.CheckCurrentWeather;
using WeatherCheck.Application.Options;
using WeatherCheck.Application.SeedWork.Repositories;
using WeatherCheck.WeatherStackClient.Repositories;

namespace WeatherCheck
{
    public class Startup
    {
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

            return Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureServices(ConfigureServices);

        }
        

        public static void ConfigureServices(HostBuilderContext context, IServiceCollection serviceCollection)
        {
            //add options
            serviceCollection.Configure<WeatherTypes>(context.Configuration.GetSection("WeatherTypes"));
            serviceCollection.Configure<WeatherStackConfig>(context.Configuration.GetSection("WeatherStackConfig"));

            //add services
            serviceCollection.AddTransient<WeatherCheckerService>();

            serviceCollection.AddTransient<IWeatherRepository, WeatherRepository>();

            serviceCollection.AddMediatR(typeof(CheckCurrentWeatherQuery));
            serviceCollection.AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<CheckCurrentWeatherValidator>());

            serviceCollection.AddHttpClient<IWeatherRepository, WeatherRepository>((services, httpClient) =>
            {
                var weatherStackConfig = services.GetService<IOptions<WeatherStackConfig>>()?.Value ?? throw new Exception("WeatherStackConfig not found");
                httpClient.BaseAddress = new Uri(weatherStackConfig.BaseAddress);
            }).AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(new[]
            {
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(5),
            }));

        }
    }
}
