using System.Reflection;
using MediatR;
using MetaApp.DataContracts.Command.Request;
using MetaApp.DataContracts.Command.Response;
using Microsoft.Extensions.DependencyInjection;

namespace MetaApp.Commands
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCommands(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddMediatR(Assembly.GetExecutingAssembly());
            serviceCollection.AddScoped<IRequestHandler<UpsertWeatherDataHandlerRequest, UpsertWeatherDataHandlerResponse>, UpsertWeatherDataRequestHandler>();

            return serviceCollection;
        }
    }
}