using System.ComponentModel.DataAnnotations;

namespace AstrologerMicroservice.Domain.Entities.Enums
{

    [Flags]
    public enum Languages : long
    {
        [Display(Name = "None")]
        None = 0,

        [Display(Name = "Hindi")]
        Hindi = 1 << 0,

        [Display(Name = "English")]
        English = 1 << 1,

        [Display(Name = "Tamil")]
        Tamil = 1 << 2,

        [Display(Name = "Bengali")]
        Bengali = 1 << 3,

        [Display(Name = "Marathi")]
        Marathi = 1 << 4,

        [Display(Name = "Gujarati")]
        Gujarati = 1 << 5,

        [Display(Name = "Punjabi")]
        Punjabi = 1 << 6,

        [Display(Name = "Telugu")]
        Telugu = 1 << 7,

        [Display(Name = "Kannada")]
        Kannada = 1 << 8,

        [Display(Name = "Malayalam")]
        Malayalam = 1 << 9,

        [Display(Name = "Odia")]
        Odia = 1 << 10,

        [Display(Name = "Assamese")]
        Assamese = 1 << 11,

        [Display(Name = "Urdu")]
        Urdu = 1 << 12,

        [Display(Name = "Sanskrit")]
        Sanskrit = 1 << 13,

        [Display(Name = "Kashmiri")]
        Kashmiri = 1 << 14,

        [Display(Name = "Sindhi")]
        Sindhi = 1 << 15,

        [Display(Name = "Maithili")]
        Maithili = 1L << 16,

        [Display(Name = "Dogri")]
        Dogri = 1L << 17,

        [Display(Name = "Manipuri (Meitei)")]
        Manipuri = 1L << 18,

        [Display(Name = "Bodo")]
        Bodo = 1L << 19,

        [Display(Name = "Santhali")]
        Santhali = 1L << 20
    }

}