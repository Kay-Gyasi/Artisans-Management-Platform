﻿namespace AMP.Processors.PageDtos.BusinessManagement
{
    public class PaymentPageDto
    {
        public string Id { get; set; }
        public string OrderId { get; set; }
        public decimal AmountPaid { get; set; }
        public bool IsVerified { get; set; }
        public bool IsForwarded { get; set; }
        public string TransactionReference { get; set; }
        public string Reference { get; set; }
        public OrderPageDto Order { get; set; }
    }
    
    public class ArtisanPaymentPageDto
    {
        public PaginatedList<PaymentPageDto> PaymentPage { get; set; }
        public double AmountInWithholding { get; set; }
        public double TotalAmountReceived { get; set; }
        public int NoOfOrdersCompleted { get; set; }
    }
}