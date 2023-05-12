namespace QueryPack.Audit
{
    using Services;

    public interface IDependencyContext
    {
        IAuditableSender Sender { get; }
    }
}
