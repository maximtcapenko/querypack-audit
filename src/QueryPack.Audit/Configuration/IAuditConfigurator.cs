namespace QueryPack.Audit.Configuration
{
    using System;
    using DispatchProxy;

    /// <summary>
    /// Interception configuration
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    /// <typeparam name="TAuditable"></typeparam>
    public interface IAuditConfigurator<TContext, TAuditable> 
        where TAuditable : class 
        where TContext : class, IDependencyContext
    {
        /// <summary>
        /// Configures interception
        /// </summary>
        /// <param name="interceptorBuilder"></param>
        /// <returns></returns>
        IAuditConfigurator<TContext, TAuditable> Add(Action<IInterceptorBuilder<TContext, TAuditable>> interceptorBuilder);
    }
}