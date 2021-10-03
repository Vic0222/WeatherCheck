using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WeatherCheck;

using IHost host = Startup.CreateHostBuilder(args).Build();
var weatherCheckerService = host.Services.GetRequiredService<IWeatherCheckerService>();
await weatherCheckerService.CheckCurrentWeatherAsync();