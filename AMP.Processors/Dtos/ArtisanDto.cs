﻿using System.Collections.Generic;
using AMP.Domain.Enums;

namespace AMP.Processors.Dtos
{
    public class ArtisanDto
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public BusinessType Type { get; set; }
        
        /// <summary>
        /// Energy Commission Certification Number (for electricians only)
        /// </summary>
        public string Eccn { get; set; }

        public string BusinessName { get; set; }
        public string Description { get; set; }
        public bool IsVerified { get; set; }
        public bool IsApproved { get; set; }
        public double Rating { get; set; }
        public int NoOfReviews { get; set; }
        public int NoOfOrders { get; set; }
        public UserDto User { get; set; }
        public List<ServiceDto> Services { get; set; }
        public List<RatingDto> Ratings { get; set; }
        public List<OrderDto> Orders { get; set; }
        public List<DisputeDto> Disputes { get; set; }
    }
}