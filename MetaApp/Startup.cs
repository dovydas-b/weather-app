using System.IO;
using CommandDotNet;
using CommandDotNet.Rendering;
using MetaApp.Commands;
using MetaApp.Controllers;
using MetaApp.Infrastructure;
using MetaApp.Presentation;
using MetaApp.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace MetaApp
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
                .RegisterViews();
        }
    }
}