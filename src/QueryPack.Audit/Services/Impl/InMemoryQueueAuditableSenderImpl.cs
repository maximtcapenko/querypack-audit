namespace QueryPack.Audit.Services.Impl
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Internal;

    internal class InMemoryQueueAuditableSenderImpl : IAuditableSender
    {
        private readonly AuditableQueue _auditableQueue;

        public InMemoryQueueAuditableSenderImpl(AuditableQueue auditableQueue)
        {
            _auditableQueue = auditableQueue;
        }

        public Task SendAsync<TAuditable>(TAuditable auditable) where TAuditable : class
        {
            _auditableQueue.Enqueue(auditable);
            return Task.CompletedTask;
        }

        public Task SendAsync<TAuditable>(IEnumerable<TAuditable> auditables) where TAuditable : class
        {
            _auditableQueue.Enqueue(auditables);
            return Task.CompletedTask;
        }

    }
}