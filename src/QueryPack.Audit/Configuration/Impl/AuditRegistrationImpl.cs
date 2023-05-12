namespace QueryPack.Audit.Configuration.Impl
{
    using System;
    using DispatchProxy.Extensions;
    using Microsoft.Extensions.DependencyInjection;
    using Services;
    using Services.Impl;

    internal class AuditRegistrationImpl : IAuditRegistration
    {
        private readonly IServiceCollection _services;

        public AuditRegistrationImpl(IServiceCollection services)
        {
            _services = services;
        }

        public IAuditRegistration AddReceiver<TAuditable, TReceiver>()
            where TAuditable : class
            where TReceiver : class, IAuditableReceiver<TAuditable>
        {
            _services.AddTransient<IAuditableReceiver, AuditableReceiverImpl<TAuditable>>();
            _services.AddTransient<IAuditableReceiver<TAuditable>, TReceiver>();
            return this;
        }

        public IAuditRegistration ConfigureOptions(Action<QueueReaderOptions> options)
        {
            var instance = new QueueReaderOptions();
            options?.Invoke(instance);
            _services.AddSingleton(instance);

            return this;
        }

        public IAuditRegistration AddContext<TContext>()
            where TContext : class, IDependencyContext
        {
            _services.AddTransient<TContext>();

            return this;
        }

        public IAuditRegistration AuditFor<TContext, TAuditable>(IAuditConfiguration<TContext, TAuditable> configuration)
            where TAuditable : class
            where TContext : class, IDependencyContext
        {
            var configurator = new AuditConfiguratorImpl<TContext, TAuditable>();
            configuration.Configure(configurator);
            _services.AddInterceptorFor(configurator);

            return this;
        }
    }
}