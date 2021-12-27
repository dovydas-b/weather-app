using System.Threading.Tasks;
using CommandDotNet;
using CommandDotNet.DataAnnotations;
using CommandDotNet.IoC.MicrosoftDependencyInjection;
using MetaApp.Console.Commands;
using MetaApp.Console.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace MetaApp.Console
{
    class Program
    {
        private static async Task Main(string[] args)
        {
            var services = new ServiceCollection();
            Startup.ConfigureServices(services);

            await new AppRunner<AppCommands>(new AppSettings
                {
                    Help =
                    {
                        PrintHelpOption = true,
                    },
                })
                .UseCancellationHandlers()
                .UseDataAnnotationValidations()
                .UseCommandLogger(context => Log.Information)
                .UseMicrosoftDependencyInjection(services.BuildServiceProvider())
                .RunAsync(args);
        }
    }
}