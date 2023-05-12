namespace QueryPack.Audit.Services
{
    using System.Collections.Generic;

    public interface IAuditableSender
    {
        void Send<TAuditable>(TAuditable auditable) 
            where TAuditable : class;
        void Send<TAuditable>(IEnumerable<TAuditable> auditables) 
            where TAuditable : class;
    }
}