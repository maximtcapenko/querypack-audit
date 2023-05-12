namespace QueryPack.Audit.Configuration
{
    using System;
    using Services;

    public interface IAuditRegistration
    {
        IAuditRegistration ConfigureOptions(Action<QueueReaderOptions> options);
        IAuditRegistration AddReceiver<TAuditable, TReceiver>()
            where TReceiver : class, IAuditableReceiver<TAuditable>
            where TAuditable : class;

        IAuditRegistration AddContext<TContext>() where TContext : class, IDependencyContext;
        IAuditRegistration AuditFor<TContext, TAuditable>(IAuditConfiguration<TContext, TAuditable> configuration)
            where TAuditable : class
            where TContext : class, IDependencyContext;
    }
}