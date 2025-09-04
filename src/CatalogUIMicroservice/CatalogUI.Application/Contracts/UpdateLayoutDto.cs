using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogUI.Domain.Entities;

namespace CatalogUI.Application.Contracts
{
    public class UpdateLayoutDto
    {
        public int Id { get; set; }
        public string PageName { get; set; } = string.Empty;
        public string LayoutJson { get; set; } = string.Empty;
        public string? TenantId { get; set; }
        public string? UserRole { get; set; }

        public static void UpdateEntity(Layout entity, UpdateLayoutDto dto)
        {
            entity.PageName = dto.PageName;
            entity.LayoutJson = dto.LayoutJson;
            entity.TenantId = dto.TenantId;
            entity.UserRole = dto.UserRole;
        }
    }
}