namespace QueryPack.Audit.Configuration
{
    using System;
    using DispatchProxy;

    public interface IAuditConfigurator<TContext, TAuditable> 
        where TAuditable : class 
        where TContext : class, IDependencyContext
    {
        IAuditConfigurator<TContext, TAuditable> Add(Action<IInterceptorBuilder<TContext, TAuditable>> interceptorBuilder);    }
}