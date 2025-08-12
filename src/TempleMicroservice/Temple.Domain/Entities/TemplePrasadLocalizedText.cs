using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Temple.Domain.Entities
{
    public class TemplePrasadLocalizedText
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TemplePrasadId { get; set; }

        [Required, MaxLength(5)]
        public string LanguageCode { get; set; } = "en";

        [Required, MaxLength(200)]
        public string LocalizedName { get; set; } = null!;

        [MaxLength(1000)]
        public string? LocalizedDescription { get; set; }

        [ForeignKey("TemplePrasadId")]
        public TemplePrasad TemplePrasad { get; set; } = null!;
    }

}