using System;
using System.Globalization;
using AMP.Domain.Entities;
using AMP.Processors.Commands;

namespace AMP.Processors.Payment
{
    public class MobileMoneyPayCommand
    {

        public MobileMoneyPayCommand(Customers customer, PaymentCommand paymentCommand)
        {
            FirstName = customer.User.FirstName;
            LastName = customer.User.FamilyName;
            Email = customer.User.Contact.EmailAddress;
            Amount = paymentCommand.AmountPaid;
            PhoneNumber = customer.User.MomoNumber ?? customer.User.Contact.PrimaryContact;
            SetNetwork();
            TransactionReference = string.Join("-",
                new string[]
                {
                    FirstName, LastName, Amount.ToString(CultureInfo.InvariantCulture),
                    paymentCommand.OrderId.ToString()
                });
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public decimal Amount { get; set; }
        public string PhoneNumber { get; set; }
        public string Network { get; set; }
        public string Currency { get; set; } = "GHS";
        public string Country { get; set; } = "GH";
        public string PaymentType { get; set; } = "mobilemoneygh";
        public string TransactionReference { get; set; }

        private void SetNetwork()
        {
            if (PhoneNumber.StartsWith("024") || PhoneNumber.StartsWith("054") || PhoneNumber.StartsWith("059"))
            {
                Network = "MTN";
                return;
            }
            
            if (PhoneNumber.StartsWith("020") || PhoneNumber.StartsWith("050"))
            {
                Network = "VODAFONE";
                return;
            }
            
            if (PhoneNumber.StartsWith("027") || PhoneNumber.StartsWith("057"))
            {
                Network = "TIGO";
                return;
            }

            Network = "MTN";
        }
    }
}