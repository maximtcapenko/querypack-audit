namespace QueryPack.Audit.Extensions
{
    using Microsoft.Extensions.DependencyInjection;
    using Configuration;
    using Configuration.Impl;
    using Internal;
    using Services;
    using Services.Impl;
    using DispatchProxy.Extensions;
    using System;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAudit(this IServiceCollection self,
            Action<IAuditRegistration> registrationBuilder)
        {
            self.AddHostedService<AuditableQueueReaderHostService>();
            self.AddSingleton<AuditableQueue>();
            self.AddSingleton<IAuditableReceiverResolver, AuditableReceiverResolverImpl>();
            self.AddSingleton<IAuditableSender, AuditableSenderImpl>();

            var registration = new AuditRegistrationImpl(self);
            registrationBuilder(registration);

            return self;
        }
    }
}