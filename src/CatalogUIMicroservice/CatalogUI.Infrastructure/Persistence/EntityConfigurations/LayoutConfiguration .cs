using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogUI.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CatalogUI.Infrastructure.Persistence.EntityConfigurations
{
    public class LayoutConfiguration : IEntityTypeConfiguration<Layout>
    {
        public void Configure(EntityTypeBuilder<Layout> builder)
        {
            builder.ToTable("layouts");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.Id)
                .HasColumnName("id");

            builder.Property(l => l.PageName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("page_name");

            builder.Property(l => l.LayoutJson)
                .IsRequired()
                .HasColumnType("json")
                .HasColumnName("layout_json");

            builder.Property(l => l.TenantId)
                .HasMaxLength(50)
                .IsRequired(false)
                .HasColumnName("tenant_id");

            builder.Property(l => l.UserRole)
                .HasMaxLength(50)
                .IsRequired(false)
                .HasColumnName("user_role");

            builder.Property(l => l.CreatedAt)
                .IsRequired()
                .HasColumnName("created_at");

            builder.Property(l => l.UpdatedAt)
                .IsRequired(false)
                .HasColumnName("updated_at");

            builder.Property(l => l.IsActive)
                .HasDefaultValue(true)
                .IsRequired()
                .HasColumnName("is_active");

            builder.Property(l => l.Version)
                .HasDefaultValue(1)
                .IsRequired()
                .HasColumnName("version");

            builder.Property(l => l.CreatedBy)
                .HasMaxLength(100)
                .IsRequired(false)
                .HasColumnName("created_by");

            builder.Property(l => l.UpdatedBy)
                .HasMaxLength(100)
                .IsRequired(false)
                .HasColumnName("updated_by");

            builder.Property(l => l.Description)
                .HasMaxLength(500)
                .IsRequired(false)
                .HasColumnName("description");

            builder.HasIndex(l => new { l.PageName, l.TenantId, l.UserRole })
                .HasDatabaseName("ix_layouts_page_tenant_role");

            builder.HasIndex(l => new { l.PageName, l.TenantId, l.UserRole, l.Version })
                .IsUnique()
                .HasDatabaseName("ux_layouts_page_tenant_role_version");
        }
    }
}