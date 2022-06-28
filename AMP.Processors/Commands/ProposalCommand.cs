namespace AMP.Processors.Commands
{
    public class ProposalCommand
    {
        public int OrderId { get; set; }
        public int ArtisanId { get; set; }
        public bool IsAccepted { get; set; }
    }
}