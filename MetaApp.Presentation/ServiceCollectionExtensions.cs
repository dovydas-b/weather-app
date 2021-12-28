using MetaApp.Infrastructure.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace MetaApp.Presentation
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterViews(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IConsoleView, WeatherDataConsoleView>();

            return serviceCollection;
        }
    }
}