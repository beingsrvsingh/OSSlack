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

        public string BookingReferenceNumber { get; set; } = Guid.NewGuid().ToString();

        // Temple, Priest, Event, etc.
        [Required]
        [MaxLength(50)]
        public string EntityType { get; set; } = null!;

        [Required]
        public int EntityId { get; set; }
        
        public required string UserId { get; set; }

        public string ProductName { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

        //app, event
        [MaxLength(50)]
        public string Source { get; set; } = "web";

        [Column(TypeName = "json")]
        public string? BookingOptionsJson { get; set; }

        [Required]
        public BookingStatus Status { get; set; } = BookingStatus.Pending;

        [MaxLength(500)]
        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } 
    }

    public enum BookingStatus
    {
        Pending,
        Confirmed,
        Cancelled,
        Completed
    }

}