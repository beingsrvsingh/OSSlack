using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Identity.Domain.Entities;

namespace Identity.Domain.Entities;

public class AspNetUserMembership
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity), Column(name: "id")]
    public int Id { get; set; }

    [Required, Column(name: "user_id")]
    [StringLength(450)]
    public string UserId { get; set; } = null!;

    public MembershipType MembershipType { get; set; } // e.g., "Gold", "Silver", etc.

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public bool IsActive => DateTime.UtcNow >= StartDate && DateTime.UtcNow <= EndDate;

    [ForeignKey(nameof(UserId))]
    public virtual ApplicationUser User { get; set; } = null!;
}
