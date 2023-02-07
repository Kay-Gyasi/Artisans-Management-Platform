using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using AMP.Processors.Services.Payments;
using Microsoft.Extensions.Logging;
using PayStack.Net;
using PhoneNumbers;

namespace AMP.Services.Payments
{
    public class PaymentService : IPaymentService
    {
        private readonly ILogger<PaymentService> _logger;
        private readonly PayStackApi _api;

        public PaymentService(IConfiguration configuration, ILogger<PaymentService> logger)
        {
            _logger = logger;
            _api = new PayStackApi(configuration["PaystackTest_SecretKey"]);
        }

        public async Task<(int?, string, DateTime?, DateTime?)> CreateCustomer(string firstname, string lastname, string phone)
        {
            try
            {
                var response = await Task.Run(() => _api.Transfers.Recipients
                    .Create(new CreateTransferRecipientRequest
                    {
                        Name = "Kofi Gyasi",
                        Type = "mobile_money",
                        Currency = "GHS"
                    })); // try using your bank account to create
                if (response.Status)
                    return (response.Data.Id, response.Data.RecipientCode,
                        response.Data.CreatedAt, response.Data.UpdatedAt);
                return (null, null, null, null);
            }
            catch (Exception e)
            {
                _logger.LogError("Payment customer creation failed with message: {Message}", e.Message);
                return (null, null, null, null);
            }
        }
        
        public async Task ForwardPayment(int amount, string recipientCode)
        {
            var response = _api.Transfers
                .InitiateTransfer(amount, recipientCode,
                    currency: "GHS", reason: "Payments forwarding");
        }
    }
}