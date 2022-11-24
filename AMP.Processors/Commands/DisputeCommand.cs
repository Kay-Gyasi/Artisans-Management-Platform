namespace AMP.Processors.Commands
{
    public class DisputeCommand
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public DisputeStatus Status { get; set; }
        public string OrderId { get; set; }
        public string Details { get; set; }
    }

    public class DisputeCount
    {
        public int Count { get; set; }
    }
}