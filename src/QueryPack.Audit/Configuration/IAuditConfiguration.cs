namespace QueryPack.Audit.Configuration
{
    public interface IAuditConfiguration<TContext, TAuditable> 
        where TAuditable : class
        where TContext : class, IDependencyContext
    {
        void Configure(IAuditConfigurator<TContext, TAuditable> configurator);
    }
}