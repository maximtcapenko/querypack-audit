namespace QueryPack.Audit.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Extensions.DependencyInjection;
    using Services;

    internal class AuditableReceiverResolverImpl : IAuditableReceiverResolver
    {
        private readonly IServiceProvider _serviceProvider;

        public AuditableReceiverResolverImpl(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IEnumerable<IAuditableReceiver> Resolve(Type type)
            => _serviceProvider.GetServices<IAuditableReceiver>()?.Where(e => e.CanReceive(type));
    }
}