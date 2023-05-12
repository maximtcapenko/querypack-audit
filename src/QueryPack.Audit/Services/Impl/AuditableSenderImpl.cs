namespace QueryPack.Audit.Services.Impl
{
    using System.Collections.Generic;
    using Internal;

    internal class AuditableSenderImpl : IAuditableSender
    {
        private readonly AuditableQueue _auditableQueue;
        
        public AuditableSenderImpl(AuditableQueue auditableQueue)
        {
            _auditableQueue = auditableQueue;
        }

        public void Send<TAuditable>(TAuditable auditable) where TAuditable : class 
            => _auditableQueue.Enqueue(auditable);

        public void Send<TAuditable>(IEnumerable<TAuditable> auditables) where TAuditable : class 
            => _auditableQueue.Enqueue(auditables);
    }
}