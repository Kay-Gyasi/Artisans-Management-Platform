using System;
using System.Threading.Tasks;

namespace AMP.Processors.Services.Payments;

public interface IPaymentService
{
    Task<(int?, string, DateTime?, DateTime?)> CreateCustomer(string firstname, string lastname, string phone);
}