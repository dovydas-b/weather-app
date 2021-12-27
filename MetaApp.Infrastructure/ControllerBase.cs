using System;
using System.Threading;

namespace MetaApp.Infrastructure
{
    public class ControllerBase
    {
        public void ExitOnCancelKeyPress(CancellationToken cancellationToken)
        {
            Console.CancelKeyPress += (_, _) =>
                CancellationTokenSource.CreateLinkedTokenSource(cancellationToken).Cancel();

            cancellationToken.WaitHandle.WaitOne();
        }
    }
}