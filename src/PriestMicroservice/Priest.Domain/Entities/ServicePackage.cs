using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Priest.Domain.Entities
{
    public class ServicePackage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PriestId { get; set; }

        [Required, MaxLength(150)]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        [Required, Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }

        // Duration in minutes
        public int DurationInMinutes { get; set; }

        [ForeignKey(nameof(PriestId))]
        public virtual PriestMaster Priest { get; set; } = null!;
    }

}