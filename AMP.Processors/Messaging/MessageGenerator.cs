namespace AMP.Processors.Messaging;

public class MessageGenerator
{
    private readonly IUnitOfWork _uow;
    private static string _baseAddress => "https://www.tukofix.com";

    public MessageGenerator(IUnitOfWork uow)
    {
        _uow = uow;
    }
    
    public async Task<(string, string)> AssignArtisan(string orderId, string artisanId)
    {
        var url = new Uri($"{_baseAddress}/main/artisans/requests");
        var builder = new StringBuilder();
        var artisan = await _uow.Artisans.GetAsync(artisanId);
        var customer = (await _uow.Orders.GetAsync(orderId)).Customer.User.DisplayName;

        builder.Append($"Hello {artisan.User.FirstName}, ");
        builder.Append($"You have received a new job request from {customer}. ");
        builder.Append($"View it at {url}");

        return (builder.ToString(), FormatNumber(new[] {artisan.User.Contact.PrimaryContact}));
    }
    
    public async Task<(string, string)> UnassignArtisan(string orderId)
    {
        var builder = new StringBuilder();
        var order = await _uow.Orders.GetAsync(orderId);

        builder.Append($"Hello {order.Artisan.User.FirstName}, ");
        builder.Append(
            $"You have been unassigned from order with reference No. {order.ReferenceNo} by {order.Customer.User.DisplayName}");

        return (builder.ToString(), FormatNumber(new []{order.Artisan.User.Contact.PrimaryContact}));
    }

    public async Task<(string, string)> AcceptRequest(string orderId)
    {
        var order = await _uow.Orders.GetAsync(orderId);
        var builder = new StringBuilder();
        builder.Append($"Hello {order.Customer.User.FirstName}, ");
        builder.Append(
            $"Your order with reference No. {order.ReferenceNo} has been accepted by {order.Artisan.BusinessName}");
        return (builder.ToString(), FormatNumber(new []{order.Customer.User.Contact.PrimaryContact}));
    }

    // Optimize for speed
    public async Task<(string,string)> CustomerPaymentVerified(string trxRef)
    {
        var payment = await _uow.Payments.GetByTrxRef(trxRef);
        var builder = new StringBuilder();
        var customer = await _uow.Customers.GetAsync(payment.Order.CustomerId);
        var businessName = (await _uow.Artisans.GetAsync(payment.Order.ArtisanId)).BusinessName;
        builder.Append($"Hello {customer.User.FirstName}, ");
        builder.Append($"your payment of GHS {payment.AmountPaid} to {businessName} has been processed successfully. ");
        builder.Append("Amount received will be in withholding till you complete the order. ");
        builder.Append($"Your transaction's reference No. is {payment.TransactionReference}");
        return (builder.ToString(), FormatNumber(new []{customer.User.Contact.PrimaryContact}));
    }
    
    public async Task<(string,string)> ArtisanPaymentVerified(string trxRef)
    {
        var payment = await _uow.Payments.GetByTrxRef(trxRef);
        var builder = new StringBuilder();
        var artisan = await _uow.Artisans.GetAsync(payment.Order.ArtisanId);    
        var customerName = (await _uow.Customers.GetAsync(payment.Order.CustomerId)).User.DisplayName;
        builder.Append($"Hello {artisan.User.FirstName}, ");
        builder.Append($"{customerName} has made a payment of GHS {payment.AmountPaid} to you. ");
        builder.Append("Amount received will be forwarded to you after the order is completed. ");
        builder.Append($"The transaction's reference No. is {payment.TransactionReference}");
        return (builder.ToString(), FormatNumber(new []{artisan.User.Contact.PrimaryContact}));
    }

    public static (string, string) SendVerificationLink(string phone, string code)
    {
        var url = new Uri($"{_baseAddress}/registration/{phone}/{code}");
        var message = $"Use this link to activate your AMP user account. {url}";
        return (message, FormatNumber(new []{phone}));
    }
    
    public static (string, string) SendPasswordResetLink(string phone, string confirmCode, string name)
    {
        var url = new Uri($"{_baseAddress}/reset-password/{phone}/{confirmCode}");
        var message = $"Hello {name}, use this link to reset your password. {url}";
        return (message, FormatNumber(new []{phone}));
    }

    public static (string, string) SendInvite(InvitationCommand command, string userDisplayName)
    {
        var url = new Uri($"{_baseAddress}/");
        var message =
            $"{userDisplayName} invites you to join AMP{GetType(command.Type)}. Use this link {url} to go to the app.";

        return (message, FormatNumber(new[] {command.InvitedPhone}));
    }
    
    private static string FormatNumber(IEnumerable<string> recipients)
    {
        var pn = PhoneNumberUtil.GetInstance().Parse(recipients.First(), "GH");
        var internationalNumber = PhoneNumberUtil.GetInstance().Format(pn, PhoneNumberFormat.INTERNATIONAL);
        return internationalNumber;
    }

    private static string GetType(UserType type)
    {
        return type switch
        {
            UserType.Artisan => " as an artisan",
            UserType.Customer => " as a customer",
            _ => ""
        };
    }
}