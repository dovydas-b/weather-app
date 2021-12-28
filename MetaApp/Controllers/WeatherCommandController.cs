using System;
using System.Linq;
using System.Threading;
using CommandDotNet;
using MediatR;
using MetaApp.DataContracts.Command.Request;
using MetaApp.DataContracts.Command.Response;
using MetaApp.Infrastructure;
using MetaApp.Infrastructure.Contracts;
using MetaApp.RequestModel;
using Microsoft.Extensions.Logging;

namespace MetaApp.Controllers
{
    [Command(Description = "Gets weather data for city",
        UsageLines = new[] { "weather --city city1,city2,...,cityn" })]
    public class WeatherCommandController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IConsoleViewPrinter consoleViewPrinter;
        private readonly ILogger<WeatherCommandController> logger;

        public const string CommandName = "weather";
        private readonly int getWeatherRefreshRateInSeconds = 30;

        public WeatherCommandController(IMediator mediator, 
            IConsoleViewPrinter consoleViewPrinter, 
            ILogger<WeatherCommandController> logger)
        {
            this.mediator = mediator;
            this.consoleViewPrinter = consoleViewPrinter;
            this.logger = logger;
        }

        [DefaultCommand]
        public void GetWeatherData(GetWeatherDataRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"{CommandName} has started");

            Utils.CreateRecurringAction(() =>
            {
                consoleViewPrinter.PrintView(CommandName, cancellationToken);

                request.Cities
                    .Distinct()
                    .AsParallel()
                    .AsOrdered()
                    .WithCancellation(cancellationToken)
                    .ForAll(async city =>
                    {
                        await mediator.Send<UpsertWeatherDataHandlerResponse>(new UpsertWeatherDataHandlerRequest
                        {
                            CityName = city
                        }, cancellationToken);
                    });

            }, TimeSpan.FromSeconds(getWeatherRefreshRateInSeconds).TotalMilliseconds);

            this.ExitOnCancelKeyPress(cancellationToken);
        }
    }
}