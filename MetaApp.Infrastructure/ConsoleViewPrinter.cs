using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MetaApp.Infrastructure.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace MetaApp.Infrastructure
{
    public class ConsoleViewPrinter : IConsoleViewPrinter
    {
        private readonly IServiceProvider serviceProvider;

        public ConsoleViewPrinter(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public Task PrintView(string type, CancellationToken cancellationToken)
        {
            var consolePrinter = serviceProvider
                .GetServices<IConsoleView>()
                .FirstOrDefault(x => x.Type == type);

            if (consolePrinter == null)
            {
                throw new ArgumentNullException($"Unable to find {type} console printer");
            }

            return consolePrinter.PrintView(cancellationToken);
        }
    }
}
