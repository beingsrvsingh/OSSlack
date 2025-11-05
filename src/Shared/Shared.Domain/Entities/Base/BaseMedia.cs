using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Domain.Entities.Base
{
    public partial class BaseMedia
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(300)]
        public string ImageUrl { get; set; } = null!;

        // Image, Video, PDF, etc.
        public MediaType MediaType { get; set; } = MediaType.Image;

        [MaxLength(50)]
        public string? AltText { get; set; }

        public int SortOrder { get; set; } = 0;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
