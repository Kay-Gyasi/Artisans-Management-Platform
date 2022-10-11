using System;
using System.Text;
using System.Threading.Tasks;
using AMP.Processors.Repositories.UoW;

namespace AMP.Processors.Workers;

public class MessageGenerator
{
    private readonly IUnitOfWork _uow;

    public MessageGenerator(IUnitOfWork uow)
    {
        _uow = uow;
    }
    
    public async Task<(string, string)> AssignArtisan(string orderId, string artisanId)
    {
        var url = new Uri("https://artisan-management-platform.com/artisans/requests");
        var builder = new StringBuilder();
        var artisan = await _uow.Artisans.GetAsync(artisanId);
        var customer = (await _uow.Orders.GetAsync(orderId)).Customer.User.DisplayName;

        builder.Append($"Hello {artisan.User.FirstName}, ");
        builder.Append($"You have received a new job request from {customer}. ");
        builder.Append($"View it at {url}");

        return (builder.ToString(), artisan.User.Contact.PrimaryContact);
    }
    
    public async Task<(string, string)> UnassignArtisan(string orderId)
    {
        var builder = new StringBuilder();
        var order = await _uow.Orders.GetAsync(orderId);

        builder.Append($"Hello {order.Artisan.User.FirstName}, ");
        builder.Append(
            $"You have been unassigned from order with reference No. {order.ReferenceNo} by {order.Customer.User.DisplayName}");

        return (builder.ToString(), order.Artisan.User.Contact.PrimaryContact);
    }

    public async Task<(string, string)> AcceptRequest(string orderId)
    {
        var order = await _uow.Orders.GetAsync(orderId);
        var builder = new StringBuilder();
        builder.Append($"Hello {order.Customer.User.FirstName}, ");
        builder.Append(
            $"Your order with reference No. {order.ReferenceNo} has been accepted by {order.Artisan.BusinessName}");
        return (builder.ToString(), order.Customer.User.Contact.PrimaryContact);
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
        return (builder.ToString(), customer.User.Contact.PrimaryContact);
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
        return (builder.ToString(), artisan.User.Contact.PrimaryContact);
    }
}