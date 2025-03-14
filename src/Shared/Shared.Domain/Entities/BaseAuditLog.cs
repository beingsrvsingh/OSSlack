namespace Shared.Domain.Entities
{
    public class BaseAuditLog : BaseEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public string TableName { get; set; } = null!;
        public string Action { get; set; } = null!;
        public string TableId { get; set; } = null!;
        public string? OldValues { get; set; }
        public string? NewValues { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}