using MetaApp.DataContracts.Dtos;
using MetaApp.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MetaApp.Presentation
{
    public class WeatherDataConsoleView : IConsoleView
    {
        public string Type { get; set; } = "weather";

        private readonly IMessageQueue messageQueue;

        private const int DefaultCellSpace = -15;

        private readonly List<string> tableHeaders = new()
        {
            "City",
            "Weather",
            "Temperature",
            "Precipitation"
        };

        public WeatherDataConsoleView(IMessageQueue messageQueue)
        {
            this.messageQueue = messageQueue;
        }

        public Task PrintView(CancellationToken cancellationToken)
        {
            var headerValues = tableHeaders.Select(x => $"{x,DefaultCellSpace}");
            var header = string.Join('|', headerValues);
            var line = $"|{string.Join("", header.Select(x => "_")),60}|";

            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"Last updated on {DateTime.Now}");
            Console.WriteLine(line);
            Console.WriteLine($"|{header}|");
            Console.WriteLine(line);

            messageQueue.Dequeue<WeatherDto>((model) =>
            {
                Console.WriteLine(
                    $"|{model.City,DefaultCellSpace}|" +
                    $"{model.Weather ?? "-",DefaultCellSpace}|" +
                    $"{model.Temperature?.ToString() ?? "-",DefaultCellSpace}|" +
                    $"{model.Precipitation?.ToString() ?? "-",DefaultCellSpace}|");
            }, cancellationToken);

            return Task.CompletedTask;
        }
    }
}