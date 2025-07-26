using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Identity.Domain.Entities;

namespace Identity.Domain.Entities;

public partial class AspNetUserAddress
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity), Column(name: "id")]
    public int Id { get; set; }

    [Required, Column(name: "user_id")]
    public required string UserId { get; set; }

    [Column(name: "street_address")]
    public required string StreetAddress { get; set; }

    [Column(name: "apartment_address")]
    public string ApartmentSuitUnitAddress { get; set; } = null!;

    [Required, Column(name: "city")]
    public required string City { get; set; }

    [Required, Column(name: "state")]
    public required string State { get; set; }

    [Required, Column(name: "country")]
    public required string Country { get; set; }

    [Required, Column(name: "zip_code")]
    public required string ZipCode { get; set; }

    [Column(name: "is_deleted")]
    public bool IsDeleted { get; set; } = true;

    [Column(name: "is_default")]
    public bool IsDefault { get; set; } = true;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [ForeignKey(nameof(UserId))]
    public virtual ApplicationUser? User { get; set; }
}
