using System.ComponentModel.DataAnnotations;

namespace JwtTokenAuthentication.Domain.Entities
{
    public partial class AspNetUserSecurityToken
    {
        [Key]
        public required string UserId { get; set; }
        public required string SecurityKey { get; set; }
    }


}
