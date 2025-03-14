using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;
using Shared.Domain.Common.Entities.Interface;

namespace Identity.Domain.Entities
{
    public class ApplicationUser : IdentityUser, IActiveEntity, ISoftDeleteEntity
    {        
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        [InverseProperty(nameof(AspNetUserInfo.ApplicationUser))]
        public ICollection<AspNetUserInfo> AspNetUsers { get; set; }
    }
}
