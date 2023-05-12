namespace QueryPack.Audit.Services
{
    using System.Collections.Generic;

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
        void Send<TAuditable>(TAuditable auditable) 
            where TAuditable : class;
        /// <summary>
        /// Sends a batch of objects
        /// </summary>
        /// <typeparam name="TAuditable"></typeparam>
        /// <param name="auditables"></param>
        void Send<TAuditable>(IEnumerable<TAuditable> auditables) 
            where TAuditable : class;
    }
}