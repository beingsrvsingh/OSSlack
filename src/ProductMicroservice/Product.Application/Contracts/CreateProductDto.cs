using System.ComponentModel.DataAnnotations;

namespace Product.Application.Contracts
{
    public class CreateProductDto
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Sku { get; set; } = null!;

        public string Description { get; set; } = null!;
        public bool IsActive { get; set; }
    }

}