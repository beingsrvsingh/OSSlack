using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogUI.Domain.Entities
{
    public class Layout
    {
        public int Id { get; set; }
        public string PageName { get; set; } = "";
        public string LayoutJson { get; set; } = "";
        public string? TenantId { get; set; }
        public string? UserRole { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;
        public int Version { get; set; } = 1;
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public string? Description { get; set; }
    }

}