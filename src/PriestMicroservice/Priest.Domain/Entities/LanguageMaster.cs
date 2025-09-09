
using System.ComponentModel.DataAnnotations;

namespace PriestMicroservice.Domain.Entities
{
    public class LanguageMaster
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(50)]
        public string Code { get; set; } = string.Empty;
        public int DisplayOrder { get; set; }

        public ICollection<PriestLanguage> PriestLanguages { get; set; } = new List<PriestLanguage>();

    }

}