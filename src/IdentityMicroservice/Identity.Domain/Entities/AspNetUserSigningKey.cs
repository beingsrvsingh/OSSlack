using System.ComponentModel.DataAnnotations.Schema;
using Identity.Domain.Entities;
using Shared.Domain.Enums;

namespace JwtTokenAuthentication.Domain.Entities
{
    public partial class AspNetUserSigningKey
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string UserId { get; set; } = null!;
        public string? SigningHash { get; set; }
        public string? EncryptedSigningKey { get; set; } // Store per-user signing key (encrypted)
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ExpiresAt { get; set; }
        public DateTime? LastUsedAt { get; set; }
        public bool IsRevoked { get; set; } = false;

        public string? CreatedByIp { get; set; }
        public string? UserAgent { get; set; }

        [ForeignKey(nameof(User))]
        public ApplicationUser? User { get; set; }
    }
}
