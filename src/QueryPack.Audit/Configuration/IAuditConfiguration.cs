namespace QueryPack.Audit.Configuration
{
    /// <summary>
    /// Configures audit for user defined types
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    /// <typeparam name="TAuditable"></typeparam>
    public interface IAuditConfiguration<TContext, TAuditable> 
        where TAuditable : class
        where TContext : class, IDependencyContext
    {
        /// <summary>
        /// Configures audit conponents
        /// </summary>
        /// <param name="configurator"></param>
        void Configure(IAuditConfigurator<TContext, TAuditable> configurator);
    }
}