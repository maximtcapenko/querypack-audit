namespace QueryPack.Audit
{
    /// <summary>
    /// Configuration items for auditable queue
    /// </summary>
    public class QueueReaderOptions
    {
        /// <summary>
        /// Max size of batch read default is 100
        /// </summary>
        public int QueueReadBatchSize { get; set; } = 100;
        /// <summary>
        /// Interval of reading data from queue default is 2 seconds
        /// </summary>
        public int QueueReadIntervalInSeconds { get; set; } = 2;
        /// <summary>
        /// Max timeout of receive operation default is 10 seconds
        /// </summary>
        public int ReceiveTimeoutInSeconds { get; set; } = 10;
    }
}