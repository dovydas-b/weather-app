using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using MetaApp.Infrastructure.Contracts;

namespace MetaApp.Infrastructure
{
    public class MessageQueue : IMessageQueue
    {
        private readonly BlockingCollection<object> messageQueue;

        public MessageQueue()
        {
            this.messageQueue = new BlockingCollection<object>();
        }

        public void Dequeue<TValue>(Action<TValue> onResult, CancellationToken cancellationToken) where TValue : class
        {
            Task.Run(() =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var obj = messageQueue.Take(cancellationToken);
                    onResult(obj as TValue);
                }
            }, cancellationToken);
        }

        public void Queue<T>(T obj, CancellationToken cancellationToken)
        {
            messageQueue.Add(obj, cancellationToken);
        }
    }
}
