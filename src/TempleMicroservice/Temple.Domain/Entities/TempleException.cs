
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Temple.Domain.Entities
{
    public class TempleException
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TempleMasterId { get; set; }

        [Required]
        public DateTime ExceptionDate { get; set; }

        public bool IsClosed { get; set; }

        public TimeSpan? OpenTime { get; set; }  // Null means closed whole day

        public TimeSpan? CloseTime { get; set; } // Null means closed whole day

        public string? Reason { get; set; }

        [ForeignKey(nameof(TempleMasterId))]
        public virtual TempleMaster TempleMaster { get; set; } = null!;
    }
}