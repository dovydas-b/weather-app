using MetaApp.Infrastructure.ConsolePrinter;
using MetaApp.Infrastructure.Contracts;
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

        public static IServiceCollection AddWeatherApi(this IServiceCollection serviceCollection)
        {
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