using System.IO;
using CommandDotNet;
using CommandDotNet.Rendering;
using MetaApp.Commands;
using MetaApp.Console.Commands;
using MetaApp.Console.Controllers;
using MetaApp.Console.View;
using MetaApp.DataContracts.Configuration;
using MetaApp.Infrastructure;
using MetaApp.Infrastructure.Contracts;
using MetaApp.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace MetaApp.Console
{
    public static class Startup
    {
        public static void ConfigureServices(IServiceCollection serviceCollection)
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json", false)
                .Build();

            serviceCollection
                .AddLogging(builder =>
                {
                    var logger = new LoggerConfiguration()
                        .ReadFrom.Configuration(config)
                        .Enrich.FromLogContext()
                        .WriteTo.RollingFile("log.txt")
                        .CreateLogger();

                    builder.AddSerilog(dispose: true, logger: logger);
                })
                .AddCommands()
                .AddRepositories(config)
                .AddMessageQueue()
                .AddSingleton(typeof(WeatherCommandController))
                .AddSingleton<IConsole, SystemConsole>()
                .AddConsoleViewPrinter()
                .AddSingleton<IConsoleView, WeatherDataConsoleView>();

            serviceCollection.Configure<WeatherApiConfiguration>(config
                .GetSection(nameof(WeatherApiConfiguration)));
        }
    }
}