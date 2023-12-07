namespace QueryPack.Audit.Extensions
{
    using System;
    using Configuration;
    using Configuration.Impl;
    using Internal;
    using Microsoft.Extensions.DependencyInjection;
    using Services;
    using Services.Impl;

    /// <summary>
    /// Service collection extensions
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add audit components to DI container
        /// </summary>
        /// <param name="self"></param>
        /// <param name="registrationBuilder"></param>
        public static IServiceCollection AddAudit(this IServiceCollection self,
            Action<IAuditRegistration> registrationBuilder)
        {
            self.AddHostedService<AuditableQueueReaderHostService>();
            self.AddSingleton<AuditableQueue>();
            self.AddSingleton<IAuditableReceiverResolver, AuditableReceiverResolverImpl>();
            self.AddSingleton<IAuditableSender, InMemoryQueueAuditableSenderImpl>();

            var registration = new AuditRegistrationImpl(self);
            registrationBuilder(registration);

            return self;
        }
    }
}