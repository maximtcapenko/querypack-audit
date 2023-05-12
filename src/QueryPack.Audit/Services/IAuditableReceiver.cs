namespace QueryPack.Audit.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IAuditableReceiver
    {
        bool CanReceive(Type auditableType);
        Task<IEnumerable<object>> ReceiveAsync(IEnumerable<object> auditables, CancellationToken cancellationToken);    }

    public interface IAuditableReceiver<TAuditable> where TAuditable : class
    {
        Task<IEnumerable<TAuditable>> ReceiveAsync(IEnumerable<TAuditable> auditables, CancellationToken cancellationToken);
    }
}