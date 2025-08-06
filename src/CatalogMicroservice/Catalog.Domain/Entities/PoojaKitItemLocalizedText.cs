using System.ComponentModel.DataAnnotations;

namespace Catalog.Domain.Entities
{
    public class PoojaKitItemLocalizedText
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PoojaKitId { get; set; }

        [Required, MaxLength(5)]
        public string LanguageCode { get; set; } = "en";

        [MaxLength(150)]
        public string? LocalizedName { get; set; }

        public string? LocalizedDescription { get; set; }

        public PoojaKitItemMaster PoojaKitItem { get; set; } = null!;
    }
}