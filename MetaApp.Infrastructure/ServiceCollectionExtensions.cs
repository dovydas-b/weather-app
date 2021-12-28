using MetaApp.DataContracts.Configuration;
using MetaApp.Infrastructure.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MetaApp.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMessageQueue(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IMessageQueue, MessageQueue>();

            return serviceCollection;
        }

        public static IServiceCollection AddWeatherApi(this IServiceCollection serviceCollection, IConfiguration config)
        {
            serviceCollection.Configure<WeatherApiConfiguration>(config
                .GetSection(nameof(WeatherApiConfiguration)));

            serviceCollection.AddSingleton<IWeatherApiClient, WeatherApiClient>();

            return serviceCollection;
        }

        public static IServiceCollection AddConsoleViewPrinter(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IConsoleViewPrinter, ConsoleViewPrinter>();

            return serviceCollection;
        }
    }
}