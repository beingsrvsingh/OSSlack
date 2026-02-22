namespace Shared.Application.Common.Contracts.Response
{
    public class BookingResponseDto
    {
        /// <summary>
        /// Booking Id
        /// </summary>
        public int BookingId { get; set; }

        /// <summary>
        /// Entity Id
        /// </summary>
        public int EntityId { get; set; }

        /// <summary>
        /// Booking start time
        /// </summary>
        public TimeSpan StartTime { get; set; }

        /// <summary>
        /// Booking end time
        /// </summary>
        public TimeSpan EndTime { get; set; }
    }
}
