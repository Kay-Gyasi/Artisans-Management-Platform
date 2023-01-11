﻿namespace AMP.Processors.Commands.BusinessManagement
{
    public class PaymentCommand
    {
        public string Id { get; set; }
        public string OrderId { get; set; }
        public string Reference { get; set; }
        public decimal AmountPaid { get; set; }
    }
}