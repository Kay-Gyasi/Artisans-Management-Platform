﻿namespace AMP.Processors.Commands
{
    public class RatingCommand
    {
        public int Id { get; set; }
        public int ArtisanId { get; set; }
        public int CustomerId { get; set; }
        public int Votes { get; set; }
        public string Description { get; set; }
    }
}