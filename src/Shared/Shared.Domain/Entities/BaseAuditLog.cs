using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Domain.Entities
{
public class BaseAuditLog : BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string? UserId { get; set; }

    [Required]
    public string TableName { get; set; } = null!;

    [Required]
    public AuditAction Action { get; set; }

    [Required]
    public string TableId { get; set; } = null!;

    public string? OldValues { get; set; }

    public string? NewValues { get; set; }

    [Required]
    public DateTimeOffset CreatedOn { get; set; }

    public string? IpAddress { get; set; }
}

}