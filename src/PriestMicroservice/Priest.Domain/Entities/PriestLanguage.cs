using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Priest.Domain.Entities
{
    public class PriestLanguage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PriestId { get; set; }

        [Required, MaxLength(50)]
        public string Language { get; set; } = null!;

        [ForeignKey(nameof(PriestId))]
        public virtual PriestMaster Priest { get; set; } = null!;
    }

}