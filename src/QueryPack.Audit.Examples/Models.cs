namespace QueryPack.Audit.Examples
{
    using Services;

    class EntityResult
    {
        public string Id { get; set; }
    }

    class EntityArg { }

    class AuditContext : IDependencyContext
    {
        public AuditContext(IAuditableSender sender)
        {
            Sender = sender;
        }

        public IAuditableSender Sender { get; }
    }
}