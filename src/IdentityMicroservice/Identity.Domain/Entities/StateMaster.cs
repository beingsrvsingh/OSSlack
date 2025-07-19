using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identity.Domain.Entities;

[Table("state_master")]
public partial class StateMaster
{
    [Key]
    [Column(name: "id")]
    public int Id { get; set; }

    [Required, Column(name: "country_master_id")]
    public int CountryMasterId { get; set; }

    [Column(name: "name")]
    public string Name { get; set; } = null!;

    [Column(name: "is_active")]
    public bool IsActive { get; set; }

    [ForeignKey(nameof(CountryMasterId))]
    public virtual CountryMaster CountryMaster { get; set; } = null!;

    public virtual ICollection<CityMaster> CityMasters { get; set; } = new List<CityMaster>();
}
