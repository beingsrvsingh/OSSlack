using System.ComponentModel.DataAnnotations;

namespace Catalog.Domain.Entities
{
    public class CategoryLocalizedText
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required, MaxLength(5)]
        public string LanguageCode { get; set; } = "en";

        [MaxLength(150)]
        public string? LocalizedName { get; set; }

        public string? LocalizedDescription { get; set; }

        public CategoryMaster Category { get; set; } = null!;
    }

}