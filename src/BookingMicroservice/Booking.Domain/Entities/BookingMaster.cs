using BookingMicroservice.Domain.Entities;
using Shared.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingMicroservice.Domain.Entities
{    
    public partial class BookingMaster
    {
        [Key]
        public int Id { get; set; }

        // Polymorphic entity reference: Temple, Priest, Event, etc.
        [Required]
        [MaxLength(50)]
        public string EntityType { get; set; } = null!;

        [Required]
        public int EntityId { get; set; }

        // User who booked
        public int? UserId { get; set; }

        // Booking details
        [Required]
        [MaxLength(100)]
        public string PoojaType { get; set; } = null!;

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

        [MaxLength(50)]
        public string Source { get; set; } = "web"; //app, event

        [Required]
        public BookingStatus Status { get; set; } = BookingStatus.Pending;

        [Required]
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;

        [MaxLength(500)]
        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;    
    }

    public enum BookingStatus
    {
        Pending,
        Confirmed,
        Cancelled,
        Completed
    }

    public enum PaymentStatus
    {
        Pending,
        Paid,
        Failed,
        Refunded
    }

}