namespace QueryPack.Audit.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Sends auditable objects to a queue for further retrieval by receivers
    /// </summary>
    public interface IAuditableSender
    {
        /// <summary>
        /// Send a single object
        /// </summary>
        /// <typeparam name="TAuditable"></typeparam>
        /// <param name="auditable"></param>
        Task SendAsync<TAuditable>(TAuditable auditable) 
            where TAuditable : class;
        /// <summary>
        /// Sends a batch of objects
        /// </summary>
        /// <typeparam name="TAuditable"></typeparam>
        /// <param name="auditables"></param>
        Task SendAsync<TAuditable>(IEnumerable<TAuditable> auditables) 
            where TAuditable : class;
    }
}