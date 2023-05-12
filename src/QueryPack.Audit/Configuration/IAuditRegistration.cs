namespace QueryPack.Audit.Configuration
{
    using System;
    using Services;

    /// <summary>
    /// Registers audit components
    /// </summary>
    public interface IAuditRegistration
    {
        /// <summary>
        /// Configures and registers instance of <see cref="QueueReaderOptions"/>
        /// </summary>
        /// <param name="options"></param>
        IAuditRegistration ConfigureOptions(Action<QueueReaderOptions> options);
        /// <summary>
        /// Registers receiver of auditable object
        /// </summary>
        /// <typeparam name="TAuditable"></typeparam>
        /// <typeparam name="TReceiver"></typeparam>
        IAuditRegistration AddReceiver<TAuditable, TReceiver>()
            where TReceiver : class, IAuditableReceiver<TAuditable>
            where TAuditable : class;
        /// <summary>
        /// Registers dependency context <see cref="IDependencyContext"/>
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        IAuditRegistration AddContext<TContext>() where TContext : class, IDependencyContext;
        /// <summary>
        /// Configures interception logic
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <typeparam name="TAuditable"></typeparam>
        /// <param name="configuration"></param>
        IAuditRegistration AuditFor<TContext, TAuditable>(IAuditConfiguration<TContext, TAuditable> configuration)
            where TAuditable : class
            where TContext : class, IDependencyContext;
    }
}