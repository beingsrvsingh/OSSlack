
using System.ComponentModel.DataAnnotations;

namespace Kathavachak.Domain.Entities
{
    public class LanguageMaster
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public int DisplayOrder { get; set; }

        public virtual ICollection<KathavachakLanguage> KathavachakLanguages { get; set; } = new List<KathavachakLanguage>();
    }

}