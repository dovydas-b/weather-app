using System;
using CommandDotNet;
using MetaApp.Console.Controllers;
using MetaApp.DataContracts.Domain;
using MetaApp.Infrastructure.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MetaApp.DataContracts;

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
            var line = $"|{string.Join("", header.Select(x => "_")),60}|";

            console.Clear();
            console.SetCursorPosition(0, 0);
            console.WriteLine($"Last updated on {DateTime.Now}");
            console.WriteLine(line);
            console.WriteLine($"|{header}|");
            console.WriteLine(line);

            messageQueue.Dequeue<WeatherDto>((model) =>
            {
                console.WriteLine(
                    $"|{model.City,DefaultCellSpace}|" +
                    $"{model.Weather ?? "-",DefaultCellSpace}|" +
                    $"{model.Temperature?.ToString() ?? "-",DefaultCellSpace}|" +
                    $"{model.Precipitation?.ToString() ?? "-",DefaultCellSpace}|");
            }, cancellationToken);

            return Task.CompletedTask;
        }
    }
}