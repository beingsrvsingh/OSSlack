using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identity.Domain.Entities;

[Table("city_master")]
public partial class CityMaster
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required, Column(name: "state_master_id")]
    public int StateMasterId { get; set; }

    [Column(name: "pin_code")]
    public int Pincode { get; set; }

    [Column(name: "district_name")]
    public string DistrictName { get; set; } = null!;

    [Column(name: "area_name")]
    public string AreaName { get; set; } = null!;

    [Column(name: "is_deliverable")]
    public bool IsDeliverable { get; set; }

    [Column(name: "is_active")]
    public bool IsActive { get; set; }

    // Navigation
    [ForeignKey(nameof(StateMasterId))]
    public virtual StateMaster StateMaster { get; set; } = null!;
}
