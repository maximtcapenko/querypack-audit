namespace QueryPack.Audit.Examples
{
    using System.Threading.Tasks;
    using Extensions;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    class Program
    {
        static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();


            var service = host.Services.GetRequiredService<IEntityService>();
            var result = await service.CreateAsync(Guid.NewGuid().ToString(), new EntityArg(), CancellationToken.None);

            Console.WriteLine("Hello, World!");
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
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
    }
}
