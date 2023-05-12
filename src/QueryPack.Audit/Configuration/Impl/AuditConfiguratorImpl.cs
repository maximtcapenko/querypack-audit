namespace QueryPack.Audit.Configuration.Impl
{
    using System;
    using System.Collections.Generic;
    using DispatchProxy;

    internal class AuditConfiguratorImpl<TContext, TAuditable> : IAuditConfigurator<TContext, TAuditable>,
        InterceptorProxyFactoryBuilder<TContext, TAuditable>
        where TAuditable : class
        where TContext : class, IDependencyContext
    {
        private readonly List<Action<IInterceptorBuilder<TContext, TAuditable>>> _interceptorBuilders = new List<Action<IInterceptorBuilder<TContext, TAuditable>>>();

        public IAuditConfigurator<TContext, TAuditable> Add(Action<IInterceptorBuilder<TContext, TAuditable>> interceptorBuilder)
        {
            _interceptorBuilders.Add(interceptorBuilder);
            return this;
        }

        public void AddInterceptor(IInterceptorBuilder<TContext, TAuditable> interceptorBuilder)
        {
            foreach(var builder in _interceptorBuilders)
                builder.Invoke(interceptorBuilder);
        }
    }
}