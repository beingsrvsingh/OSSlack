using System.ComponentModel.DataAnnotations;

namespace Temple.Domain.Entities
{
    public class Expertise
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        public virtual ICollection<TempleExpertise> TempleExpertises { get; set; } = new List<TempleExpertise>();
    }

}