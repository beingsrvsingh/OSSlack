using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identity.Domain.Entities;

public partial class AspNetUserDevice
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; } = null!;

    [Required]
    public string IpAddress { get; set; } = null!;

    [Required]
    public string DeviceName { get; set; } = null!;

    [Required]
    public string Browser { get; set; } = null!;

    [Required]
    public string OS { get; set; } = null!;

    public DateTime LastAccessed { get; set; } = DateTime.UtcNow;

    public bool IsCurrent { get; set; } = false;

    public bool IsTrusted { get; set; } = false;

    [ForeignKey(nameof(UserId))]
    public virtual ApplicationUser User { get; set; } = null!;
}

