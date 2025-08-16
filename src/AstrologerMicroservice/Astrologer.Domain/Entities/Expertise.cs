using System.ComponentModel.DataAnnotations;

namespace AstrologerMicroservice.Domain.Entities
{
    public class Expertise
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        public int DisplayOrder { get; set; }
        public virtual ICollection<AstrologerExpertise> AstrologerExpertises { get; set; } = new List<AstrologerExpertise>();
    }

}