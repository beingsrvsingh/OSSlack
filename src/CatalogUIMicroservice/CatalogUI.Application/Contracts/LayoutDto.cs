using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogUI.Domain.Entities;

namespace CatalogUI.Application.Contracts
{
    public class LayoutDto
    {
        public int Id { get; set; }
        public string PageName { get; set; } = string.Empty;
        public string LayoutJson { get; set; } = string.Empty;
        public string? TenantId { get; set; }
        public string? UserRole { get; set; }
        public DateTime CreatedAt { get; set; }

        public static LayoutDto ToDto(Layout layout) => new LayoutDto
        {
            Id = layout.Id,
            PageName = layout.PageName,
            LayoutJson = layout.LayoutJson,
            TenantId = layout.TenantId,
            UserRole = layout.UserRole,
            CreatedAt = layout.CreatedAt
        };
    }
}