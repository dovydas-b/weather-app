using System.Threading;
using System.Threading.Tasks;

namespace MetaApp.Infrastructure.Contracts
{
    public interface IConsoleView
    {
        string Type { get; set; }

        Task PrintView(CancellationToken cancellationToken);
    }
}