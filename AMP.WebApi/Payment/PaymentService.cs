using AMP.Processors.Payment;
using Rave;
using Rave.Models.Charge;
using Rave.Models.MobileMoney;

namespace AMP.WebApi.Payment;

public class PaymentService : IPaymentService
{
    private readonly IConfiguration _configuration;
    private readonly string _secretKey;
    private readonly string _publicKey;

    public PaymentService(IConfiguration configuration)
    {
        _configuration = configuration;
        _secretKey = configuration["FlutterwaveTest:SecretKey"];
        _publicKey = configuration["FlutterwaveTest:PublicKey"];
    }

    public async Task PayViaMobileMoney(MobileMoneyPayCommand command)
    {
        var raveConfig = new RaveConfig(_publicKey, _secretKey, true);
        var mobilemoney = new ChargeMobileMoney(raveConfig); 

        var payload = new MobileMoneyParams(_publicKey, _secretKey, 
            command.FirstName, command.LastName, command.Email, command.Amount,
            command.Currency, command.PhoneNumber, command.Network, command.Country, command.PaymentType, 
            command.TransactionReference);
        var response = await mobilemoney.Charge(payload);
    }
}