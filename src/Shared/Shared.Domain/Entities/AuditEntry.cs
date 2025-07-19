using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;

namespace Shared.Domain.Entities
{
    public class AuditEntry(EntityEntry entry)
    {
        public EntityEntry Entry { get; } = entry;
        public string TableName { get; set; } = null!;
        public string KeyValues { get; set; } = null!;
        public AuditAction Action { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; } = null!;
        public Dictionary<string, object> OldValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> NewValues { get; } = new Dictionary<string, object>();
        public List<PropertyEntry> TemporaryProperties { get; } = new List<PropertyEntry>();

        public bool HasTemporaryProperties => TemporaryProperties.Any();

        public BaseAuditLog ToAudit()
        {
            var audit = new BaseAuditLog();
            audit.TableName = TableName;
            audit.Action = Action;
            audit.CreatedOn = CreatedOn;
            audit.UserId = CreatedBy;
            audit.TableId = KeyValues;
            audit.OldValues = OldValues.Count == 0 ? null : JsonConvert.SerializeObject(OldValues);
            audit.NewValues = NewValues.Count == 0 ? null : JsonConvert.SerializeObject(NewValues);
            return audit;
        }
    }
}