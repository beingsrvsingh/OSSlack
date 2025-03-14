using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identity.Domain.Entities
{
    public partial class AspNetUserInfo
    {
        [Key]
        [ForeignKey(nameof(ApplicationUser)), Column(Order = 0)]
        public required string UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? AvatarURI { get; set; }
        public bool IsMembership { get; set; }
        public string? MembershipId { get; set; }
        
        public ApplicationUser ApplicationUser { get; set; }
    }
}
