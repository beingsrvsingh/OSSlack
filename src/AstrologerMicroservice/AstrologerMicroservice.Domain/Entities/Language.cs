
using System.ComponentModel.DataAnnotations;

namespace AstrologerMicroservice.Domain.Entities
{
    public class Language
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<AstrologerLanguage> AstrologerLanguages { get; set; } = new List<AstrologerLanguage>();
    }

}