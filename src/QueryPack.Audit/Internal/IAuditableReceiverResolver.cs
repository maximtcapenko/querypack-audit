namespace QueryPack.Audit.Internal
{
    using System.Collections.Generic;
    using System;
    using Services;

    internal interface IAuditableReceiverResolver
    {
        IEnumerable<IAuditableReceiver> Resolve(Type type);
    }
}