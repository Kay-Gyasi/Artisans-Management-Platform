using System;

namespace AMP.Processors.Commands
{
    public class ScheduleCommand
    {
        public int OrderId { get; set; }
        public int ArtisanId { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public string Description { get; set; }
    }
}