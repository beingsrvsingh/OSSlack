using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogUI.Domain.Entities;

namespace CatalogUI.Application.Contracts
{
    public class CreateLayoutDto
    {
        public string PageName { get; set; } = string.Empty;
        public string LayoutJson { get; set; } = string.Empty;
        public string? TenantId { get; set; }
        public string? UserRole { get; set; }

        public static Layout ToEntity(CreateLayoutDto dto) => new Layout
        {
            PageName = dto.PageName,
            LayoutJson = dto.LayoutJson,
            TenantId = dto.TenantId,
            UserRole = dto.UserRole,
            CreatedAt = DateTime.UtcNow
        };
    }
}