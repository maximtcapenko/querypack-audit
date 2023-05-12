# QueryPack.Audit
Simple implementation of audit based on `QueryPack.DispatchProxy`

## Getting Started
1. Install the package into your project
```
dotnet add package QueryPack.Audit
```
2. Add audit configuration
```c#
class EntityAuditConfiguration : IAuditConfiguration<AuditContext, IEntityService>
{
    public void Configure(IAuditConfigurator<AuditContext, IEntityService> configurator)
    {
      configurator.Add(builder
        => builder.OnMethodExecuting<string, EntityArg, CancellationToken, 
            Task<EntityResult>>(e => e.CreateAsync,
          async (ctx, service, id, arg, token, invoker) =>
          {
              var result = await invoker.Invoke();
              ctx.Sender.Send(result);

              return result;
          }));
    }
 }
```
3. Register audit configuration in `Program` 
```c#
static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
      .ConfigureServices((hostContext, services) =>
      {
          services.AddTransient<IEntityService, EntityService>();
          services.AddAudit(regestry =>
          regestry.ConfigureOptions(options =>
          {
              options.QueueReadBatchSize = 50;
              options.QueueReadIntervalInSeconds = 5;
              options.ReceiveTimeoutInSeconds = 15;
          })
          .AddContext<AuditContext>()
          .AddReceiver<EntityResult, EntityResultAuditableReceiver>()
          .AuditFor(new EntityAuditConfiguration()));
      });
```
4. Implement auditable receiver
```c#
class EntityResultAuditableReceiver : IAuditableReceiver<EntityResult>
{
    public Task<IEnumerable<EntityResult>> ReceiveAsync(IEnumerable<EntityResult> auditables, CancellationToken cancellationToken)
    {
        // code is here
        // return empty collection in case if there are no errors
        return Task.FromResult(Enumerable.Empty<EntityResult>());
    }
}
```
5. Service method call
```c#
IEntityService entitySerice;
var result = await entitySerice.CreateAsync("some_id", new EntityArg(), CancellationToken.None);
```
