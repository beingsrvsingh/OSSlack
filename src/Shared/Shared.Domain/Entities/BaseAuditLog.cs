
namespace Shared.Domain.Entities;

public class BaseAuditLog : BaseEntity
{
    public int Id { get; set; }
    public string? UserId { get; set; }
    public string TableName { get; set; } = null!;
    public AuditAction Action { get; set; }
    public string TableId { get; set; } = null!;
    public string? OldValues { get; set; }
    public string? NewValues { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
    public string? IpAddress { get; set; }
}