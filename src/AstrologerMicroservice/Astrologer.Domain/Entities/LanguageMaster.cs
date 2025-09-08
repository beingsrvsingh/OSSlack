
using System.ComponentModel.DataAnnotations;

namespace AstrologerMicroservice.Domain.Entities
{
    public class LanguageMaster
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(50)]
        public string Code { get; set; } = string.Empty;
        public int DisplayOrder { get; set; }

        public ICollection<AstrologerLanguage> AstrologerLanguages { get; set; } = new List<AstrologerLanguage>();

    }

}