namespace QueryPack.Audit.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    internal interface IAuditableReceiver
    {
        bool CanReceive(Type auditableType);
        Task<IEnumerable<object>> ReceiveAsync(IEnumerable<object> auditables, CancellationToken cancellationToken);
    }

    /// <summary>
    /// Receives the auditable objects and runs user defined logic, returns failed processed objects
    /// </summary>
    /// <typeparam name="TAuditable"></typeparam>
    public interface IAuditableReceiver<TAuditable> where TAuditable : class
    {
        /// <summary>
        /// Runs runs user defined logic for auditable objects
        /// </summary>
        /// <param name="auditables"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Failed processed objects</returns>
        Task<IEnumerable<TAuditable>> ReceiveAsync(IEnumerable<TAuditable> auditables, CancellationToken cancellationToken);
    }
}