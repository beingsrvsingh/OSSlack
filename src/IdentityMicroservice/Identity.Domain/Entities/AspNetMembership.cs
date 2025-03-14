using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identity.Domain.Entities
{
    public class AspNetMembership
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required string RoleId { get; set; }
        public required string MembershipType { get; set; }
        public required int Cost { get; set; }
        public IdentityRole Role { get; set; } = null!;
    }
}
