namespace QueryPack.Audit
{
    using Services;
    /// <summary>
    /// Contains dependencies what should be injected in runtime
    /// </summary>
    public interface IDependencyContext
    {
        /// <summary>
        /// The instance of <see cref="IAuditableSender"/> what should be injectd
        /// </summary>
        IAuditableSender Sender { get; }
    }
}
