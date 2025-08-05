using System.ComponentModel.DataAnnotations;

namespace AstrologerMicroservice.Domain.Entities
{
    public class ProductItem
    {
        public int Id { get; set; }

        public int AstrologerId { get; set; }

        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        public bool IsActive { get; set; } = true;

        public Astrologer Astrologer { get; set; } = null!;
    }
}