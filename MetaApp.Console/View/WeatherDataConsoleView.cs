using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CommandDotNet;
using MetaApp.Console.Controllers;
using MetaApp.DataContracts.Domain;
using MetaApp.Infrastructure.Contracts;

namespace MetaApp.Console.View
{
    public class WeatherDataConsoleView : IConsoleView
    {
        public string Type { get; set; } = WeatherCommandController.CommandName;

        private readonly IMessageQueue messageQueue;
        private readonly IConsole console;

        private const int DefaultCellSpace = -15;

        private readonly List<string> tableHeaders = new()
        {
            "City",
            "Weather",
            "Temperature",
            "Precipitation"
        };

        public WeatherDataConsoleView(IMessageQueue messageQueue, IConsole console)
        {
            this.messageQueue = messageQueue;
            this.console = console;
        }

        public Task PrintView(CancellationToken cancellationToken)
        {
            var headerValues = tableHeaders.Select(x => $"{x,DefaultCellSpace}");
            var header = string.Join('|', headerValues);

            console.SetCursorPosition(0, 0);
            console.WriteLine($"|{header}|");
            console.WriteLine(
                $"|{string.Join("", header.Select(x => "_")),60}|");

            messageQueue.Dequeue<WeatherDataModel>((model) =>
            {
                console.WriteLine(
                    $"|{model.City,DefaultCellSpace}|" +
                    $"{model.Weather,DefaultCellSpace}|" +
                    $"{model.Temperature,DefaultCellSpace}|" +
                    $"{model.Precipitation,DefaultCellSpace}|");

            }, cancellationToken);

            return Task.CompletedTask;
        }
    }
}