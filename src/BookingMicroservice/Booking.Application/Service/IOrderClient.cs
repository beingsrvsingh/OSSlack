using Booking.Application.Contracts;
using BookingMicroservice.Domain.Entities;

namespace Booking.Application.Service
{
    public interface IOrderClient
    {
        Task<OrderResponse?> AddOrderAsync(string bookingRefNum);
        Task<bool> UpdateStatusOrderAsync(string orderNumber, string status);
    }
}
