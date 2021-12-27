using System.Threading;
using System.Threading.Tasks;

namespace MetaApp.Infrastructure.Contracts
{
    public interface IConsoleViewPrinter
    {
        Task PrintView(string type, CancellationToken cancellationToken);
    }
}