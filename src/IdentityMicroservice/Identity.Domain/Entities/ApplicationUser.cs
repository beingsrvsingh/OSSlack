using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;
using Shared.Domain.Common.Entities.Interface;

namespace Identity.Domain.Entities
{
    public class ApplicationUser : IdentityUser, IActiveEntity, ISoftDeleteEntity
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? ProfilePictureUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastLoginAt { get; set; }

        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; }

        public virtual AspNetUserMembership? Membership { get; set; }
        public virtual ICollection<AspNetUserAddress> AspNetUserAddresses { get; set; } = new List<AspNetUserAddress>();
        public virtual ICollection<AspNetUserDevice> AspNetUserDevices { get; set; } = new List<AspNetUserDevice>();
        public virtual ICollection<AspNetUserRefreshToken> AspNetUserRefreshTokens { get; set; } = new List<AspNetUserRefreshToken>();
        public virtual ICollection<AspNetUserAuditLog> AspNetUserAudits { get; set; } = new List<AspNetUserAuditLog>();

    }
}
