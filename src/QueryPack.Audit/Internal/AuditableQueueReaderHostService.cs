namespace QueryPack.Audit.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;
    using Services;

    internal class AuditableQueueReaderHostService : IHostedService, IDisposable
    {
        private Timer _timer;
        private Task _executingTask;

        private readonly QueueReaderOptions _options;
        private readonly AuditableQueue _queue;
        private readonly IAuditableReceiverResolver _receiverResolver;
        private const int DefaultServiceFirstRunAfterSeconds = 2;

        public AuditableQueueReaderHostService(AuditableQueue queue,
            QueueReaderOptions options,
            IAuditableReceiverResolver receiverResolver)
        {
            _options = options;
            _queue = queue;
            _receiverResolver = receiverResolver;
        }

        private async Task RunJobAsync()
        {
            var auditables = _queue.Dequeue(_options.QueueReadBatchSize);
            if (auditables.Any())
            {
                using var tokenSource = new CancellationTokenSource();
                tokenSource.CancelAfter(TimeSpan.FromSeconds(_options.ReceiveTimeoutInSeconds));
                var failedAudiatables = new List<object>();

                var groups = auditables.GroupBy(e => e.GetType()).ToDictionary(e => e.Key, e => e.ToList());
                foreach (var key in groups.Keys)
                {
                    var receivers = _receiverResolver.Resolve(key);
                    if (!receivers.Any())
                    {
                        _queue.Enqueue(groups[key]);
                    }

                    foreach (var receiver in receivers)
                    {
                        var results = await receiver.ReceiveAsync(groups[key], tokenSource.Token);
                        if (results.Any())
                            failedAudiatables.Add(results);
                    }
                }

                if (failedAudiatables.Any())
                {
                    _queue.Enqueue(failedAudiatables);
                }

                return;
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(ExecuteTask);
            _timer.Change(TimeSpan.FromSeconds(DefaultServiceFirstRunAfterSeconds),
                TimeSpan.FromMilliseconds(-1));
            return Task.CompletedTask;
        }

        private void ExecuteTask(object state)
        {
            _timer?.Change(Timeout.Infinite, 0);
            _executingTask = ExecuteTaskAsync();
        }

        private async Task ExecuteTaskAsync()
        {
            try
            {
                if (_executingTask == null || _executingTask.IsCompleted)
                {
                    await RunJobAsync();
                }
            }
            catch (Exception)
            {
            }

            _timer?.Change(TimeSpan.FromSeconds(_options.QueueReadIntervalInSeconds), TimeSpan.FromMilliseconds(-1));
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            if (_executingTask == null)
                return;

            await Task.WhenAny(_executingTask, Task.Delay(Timeout.Infinite, cancellationToken));
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}