using JsonFlatFileDataStore;
using MetaApp.DataContracts.Configuration;
using MetaApp.DataContracts.Repository;
using MetaApp.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MetaApp.Repository
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var configSection = configuration
                .GetSection(nameof(DatabaseConfiguration));

            serviceCollection
                .Configure<DatabaseConfiguration>(configSection)
                .AddScoped<IDataStore>(provider =>
                {
                    var config = configSection.Get<DatabaseConfiguration>();

                    return new DataStore(config.ConnectionString);
                })
                .AddScoped<IWeatherInternalRepository, WeatherInternalRepository>(); 

            serviceCollection
                .AddWeatherApi(configuration)
                .AddSingleton<IWeatherExternalRepository, WeatherExternalRepository>();

            return serviceCollection;
        }
    }
}