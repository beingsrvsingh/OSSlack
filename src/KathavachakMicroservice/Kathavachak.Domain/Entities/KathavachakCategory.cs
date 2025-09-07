using System.ComponentModel.DataAnnotations;

namespace Kathavachak.Domain.Entities
{
    public class KathavachakCategory
    {
        [Key]
        public int Id { get; set; }

        public int KathavachakId { get; set; }
        public int CategoryId { get; set; } // Foreign key to CategoryMicroservice
        public int SubCategoryId { get; set; }
        [MaxLength(100)]
        public string? SubCategoryNameSnapshot { get; set; }
        [MaxLength(100)]
        public string? CategoryNameSnapshot { get; set; }

        public KathavachakMaster Kathavachak { get; set; } = null!;
    }

}
