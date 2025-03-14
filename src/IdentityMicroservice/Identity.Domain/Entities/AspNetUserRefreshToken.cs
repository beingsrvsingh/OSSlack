using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Identity.Domain.Entities
{
    public class AspNetUserRefreshToken
    {
        [Key]
        [JsonIgnore]
        public string UserId { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public DateTime Created { get; set; }
        public string CreatedByIp { get; set; }
    }
}
