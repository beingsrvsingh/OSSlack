using BookingMicroservice.Application.Service;
using BookingMicroservice.Domain.Entities;
using BookingMicroservice.Domain.Repositories;
using Shared.Application.Interfaces.Logging;

namespace BookingMicroservice.Infrastructure.Service
{

    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _repository;
        private readonly ILoggerService<BookingService> _logger;

        public BookingService(IBookingRepository repository, ILoggerService<BookingService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<string> CreateAsync(BookingMaster booking)
        {
            await _repository.AddAsync(booking);
            await _repository.SaveChangesAsync();
            return booking.Id.ToString();
        }

        public async Task<IEnumerable<BookingMaster>> GetAvailableAsync(int bookingId)
        {
            try
            {
                return await _repository.GetAvailableAsync(bookingId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to fetch available astrologers.");
                return Enumerable.Empty<BookingMaster>();
            }
        }        
    }

}