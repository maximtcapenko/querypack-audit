namespace QueryPack.Audit.Services.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    internal class AuditableReceiverImpl<TAuditable> : IAuditableReceiver
        where TAuditable : class
    {
        private readonly IAuditableReceiver<TAuditable> _auditableReceiver;

        public AuditableReceiverImpl(IAuditableReceiver<TAuditable> auditableReceiver)
        {
            _auditableReceiver = auditableReceiver;
        }

        public bool CanReceive(Type auditableType) => typeof(TAuditable) == auditableType;

        public async Task<IEnumerable<object>> ReceiveAsync(IEnumerable<object> auditables, CancellationToken cancellationToken)
        {
            var result = await _auditableReceiver.ReceiveAsync(auditables.OfType<TAuditable>(), cancellationToken);
            return result;
        }
    }

}