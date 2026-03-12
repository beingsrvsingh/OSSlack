using Booking.Application.Contracts;

namespace Booking.Application.Service
{
    public interface IPaymentClient
    {
        Task<PaymentResponse?> Payment(string orderNumber, string userId, decimal amount);
    }
}
