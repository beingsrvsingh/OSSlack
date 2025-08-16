
using System.ComponentModel.DataAnnotations;

namespace AstrologerMicroservice.Domain.Entities
{
    public class LanguageMaster
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        public int DisplayOrder { get; set; }

        public virtual ICollection<AstrologerLanguage> AstrologerLanguages { get; set; } = new List<AstrologerLanguage>();
    }

}