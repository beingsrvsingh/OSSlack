using System.ComponentModel.DataAnnotations;

namespace StockMicroservice.Domain.Entities
{
    public class WarehouseMaster
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string Name { get; set; } = null!;

        [MaxLength(300)]
        public string? Address { get; set; }

        [MaxLength(100)]
        public string? City { get; set; }

        [MaxLength(100)]
        public string? State { get; set; }

        [MaxLength(20)]
        public string? PostalCode { get; set; }

        [MaxLength(100)]
        public string? Country { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<StockMaster> Stocks { get; set; } = new List<StockMaster>();

    }

}