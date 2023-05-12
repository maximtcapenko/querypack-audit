namespace QueryPack.Audit
{
    public class QueueReaderOptions
    {
        public int QueueReadBatchSize { get; set; } = 500;
        public int QueueReadIntervalInSeconds { get; set; } = 2;
        public int ReceiveTimeoutInSeconds { get; set; } = 10;
    }
}