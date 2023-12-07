namespace QueryPack.Audit.Examples
{
    using Configuration;

    class EntityAuditConfiguration : IAuditConfiguration<AuditContext, IEntityService>
    {
        public void Configure(IAuditConfigurator<AuditContext, IEntityService> configurator)
        {
            configurator.Add(builder
                => builder.OnMethodExecuting<string, EntityArg, CancellationToken, Task<EntityResult>>(e => e.CreateAsync,
                async (ctx, service, id, arg, token, invoker) =>
                {
                    var result = await invoker.Invoke();
                    await ctx.Sender.SendAsync(result);

                    return result;
                }));
        }
    }
}