using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kathavachak.Domain.Entities
{
    public class KathavachakMedia
    {
        [Key]
        public int Id { get; set; }

        public int KathavachakId { get; set; }

        [MaxLength(500)]
        public string Url { get; set; } = null!;

        [MaxLength(200)]
        public string? Title { get; set; }

        public MediaType MediaType { get; set; }

        public string? Description { get; set; }

        [ForeignKey(nameof(KathavachakId))]
        public virtual KathavachakMaster Kathavachak { get; set; } = null!;
    }

    public enum MediaType
    {
        Image = 1,
        Video = 2,
        Audio = 3
    }

}
