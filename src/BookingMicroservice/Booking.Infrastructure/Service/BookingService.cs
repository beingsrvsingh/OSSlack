using BookingMicroservice.Application.Service;
using BookingMicroservice.Domain.Entities;
using BookingMicroservice.Domain.Repositories;
using Shared.Application.Common.Contracts.Response;
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

        public async Task<IEnumerable<BookingResponseDto>> GetBookingsByDateAsync(int entityId, DateTime date)
        {
            try
            {
                var bookedSlots =  await _repository.GetBookingsByDateAsync(entityId, date);

                return bookedSlots.Select(b => new BookingResponseDto
                {
                    BookingId = b.Id,
                    EntityId = b.EntityId,
                    StartTime = b.StartTime,
                    EndTime = b.EndTime,
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to fetch available astrologers.");
                return Enumerable.Empty<BookingResponseDto>();
            }
        }

        public Task<IEnumerable<BookingMaster>> GetBookingByIdAsync(int bookingId)
        {
            throw new NotImplementedException();
        }
    }

}