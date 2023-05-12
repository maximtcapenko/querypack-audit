namespace QueryPack.Audit.Internal
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    internal class AuditableQueue
    {
        private readonly ConcurrentQueue<object> _auditables = new ConcurrentQueue<object>();

        public int Count() => _auditables.Count;

        public void Enqueue(object auditable)
        {
            _auditables.Enqueue(auditable);
        }

        public IEnumerable<object> Dequeue(int count)
        {
            var auditables = new List<object>();
            while (auditables.Count != count && _auditables.TryDequeue(out var auditable))
                auditables.Add(auditable);

            return auditables;
        }

        public void Enqueue(IEnumerable<object> auditables)
        {
            foreach (var auditable in auditables)
                _auditables.Enqueue(auditable);
        }
    }
}