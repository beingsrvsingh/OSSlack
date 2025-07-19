using System.ComponentModel.DataAnnotations.Schema;

namespace Identity.Domain.Entities;

[Table("country_master")]
public partial class CountryMaster
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("id")]
    public int Id { get; set; }

    [Column(name: "name")]
    public string Name { get; set; } = null!;

    [Column(name: "alpha_two_code")]
    public string AlphaTwoCode { get; set; } = null!;

    [Column(name: "emoji")]
    public string Emoji { get; set; } = null!;

    [Column(name: "uni_code")]
    public string Unicode { get; set; } = null!;

    [Column(name: "dial_code")]
    public string DialCode { get; set; } = null!;

    [Column(name: "image_uri")]
    public string ImageUri { get; set; } = null!;

    [Column(name: "is_active")]
    public bool IsActive { get; set; }
    
    public virtual ICollection<StateMaster> StateMasters { get; set; } = new List<StateMaster>();
}
