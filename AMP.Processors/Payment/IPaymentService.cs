using System.Threading.Tasks;

namespace AMP.Processors.Payment
{
    public interface IPaymentService
    {
        Task PayViaMobileMoney(MobileMoneyPayCommand command);
    }
}