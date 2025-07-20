using System.ComponentModel.DataAnnotations.Schema;
using Shared.Domain.Entities;

namespace Identity.Domain.Entities
{
    public class AuditLog : BaseAuditLog
    {
        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser? User { get; set; }
    }
}
