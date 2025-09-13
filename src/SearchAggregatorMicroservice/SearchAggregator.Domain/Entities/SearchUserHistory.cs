using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SearchAggregator.Domain.Entities
{
    public partial class UserSearchHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string UserId { get; set; } = null!;

        public string Query { get; set; } = null!;

        public string? Platform { get; set; }

        public string? Language { get; set; }

        public string? IPAddress { get; set; }

        public int? ResultCount { get; set; }

        public DateTime SearchedAt { get; set; } = DateTime.UtcNow;
    }

}
