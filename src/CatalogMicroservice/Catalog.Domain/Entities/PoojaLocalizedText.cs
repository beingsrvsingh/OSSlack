using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Domain.Entities
{
    public class PoojaLocalizedText
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PoojaMasterId { get; set; }

        [Required, MaxLength(5)]
        public string LanguageCode { get; set; } = "en";

        [MaxLength(150)]
        public string? LocalizedName { get; set; }

        public string? LocalizedDescription { get; set; }

        [ForeignKey("PoojaMasterId")]
        public virtual PoojaMaster PoojaMaster { get; set; } = null!;
    }
}