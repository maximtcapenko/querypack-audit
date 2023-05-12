namespace QueryPack.Audit.Services
{
    using System.Collections.Generic;
    using System;

    public interface IAuditableReceiverResolver
    {
        IEnumerable<IAuditableReceiver> Resolve(Type type);
    }
}