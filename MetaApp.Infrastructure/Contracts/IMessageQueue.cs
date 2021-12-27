using System;
using System.Threading;

namespace MetaApp.Infrastructure.Contracts
{
    public interface IMessageQueue
    {
        void Dequeue<TValue>(Action<TValue> onResult, CancellationToken cancellationToken) where TValue : class;

        void Queue<T>(T obj, CancellationToken cancellationToken);
    }
}