using AMP.Processors.Payment;
using Microsoft.Extensions.Configuration;
using PayStack.Net;

namespace AMP.Services.Payments
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly string _secretKey;
        private readonly string _publicKey;

        public PaymentService(IConfiguration configuration)
        {
            _configuration = configuration;
            _secretKey = configuration["PaystackTest:SecretKey"];
            _publicKey = configuration["PaystackTest:PublicKey"];
        }

        public InitializeTransactionResponse InitializeTransaction(string email, int amountInKobo, string reference = "")
        {
            var api = new PayStackApi(_secretKey);
            var response = api.Transactions.Initialize(email, amountInKobo, reference,
                false, "GHS");
            var initResponse = new InitializeTransactionResponse();
            if (response.Status)
            {
                initResponse.AccessCode = response.Data.AccessCode;
                initResponse.AuthorizationUrl = response.Data.AuthorizationUrl;
                initResponse.Reference = response.Data.Reference;
            }

            // Verifying a transaction
            var verify = api.Transactions.Verify(response.Data.Reference); // auto or supplied when initializing;
            if (!verify.Status) return initResponse;
            //You can save the details from the json object returned above so that the authorization code 
            //can be used for charging subsequent transactions
            initResponse.Channel = verify.Data.Channel;
            initResponse.Currency = verify.Data.Currency;
            initResponse.Domain = verify.Data.Domain;
            initResponse.Message = verify.Data.Message;
            initResponse.Status = verify.Data.Status;
            initResponse.TransactionDate = verify.Data.TransactionDate;
            initResponse.Amount = verify.Data.Amount;
            //Save 'authCode' for future charges!

            return initResponse;
        }
    }
}