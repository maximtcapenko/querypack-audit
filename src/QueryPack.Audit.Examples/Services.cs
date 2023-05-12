namespace QueryPack.Audit.Examples
{
    using System.Collections.Generic;
    using Services;

    interface IEntityService
    {
        Task<EntityResult> CreateAsync(string id, EntityArg arg, CancellationToken token);
        Task UpdateAsync(string id, EntityArg arg, CancellationToken token);
    }

    class EntityService : IEntityService
    {
        public Task<EntityResult> CreateAsync(string id, EntityArg arg, CancellationToken token)
        {
            Console.WriteLine("Executing method CreateAsync");
            return Task.FromResult(new EntityResult() { Id = id });
        }

        public Task UpdateAsync(string id, EntityArg arg, CancellationToken token)
        {
            Console.WriteLine("Executing method UpdateAsync");
            return Task.CompletedTask;
        }
    }

    class EntityResultAuditableReceiver : IAuditableReceiver<EntityResult>
    {
        public Task<IEnumerable<EntityResult>> ReceiveAsync(IEnumerable<EntityResult> auditables, CancellationToken cancellationToken)
        {
            Console.WriteLine("process auditables");
            foreach(var auditable in auditables)
                Console.WriteLine($"Auditable info [{auditable}]");
            Console.WriteLine("Finish");

            return Task.FromResult(Enumerable.Empty<EntityResult>());
        }
    }
}