using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pooja.Domain.Entities;

namespace Pooja.Infrastructure.Persistence.EntityConfigurations
{
    public class PoojaAddonConfiguration : IEntityTypeConfiguration<PoojaAddon>
    {
        public void Configure(EntityTypeBuilder<PoojaAddon> builder)
        {            
            builder.ToTable("pooja_addon");

            // Primary key
            builder.HasKey(a => a.Id)
                   .HasName("pk_pooja_addon");
            
            builder.Property(a => a.Id)
                .HasColumnName("id");

            builder.Property(a => a.PoojaId)
                .HasColumnName("pooja_id");

            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnName("name");

            builder.Property(a => a.Description)
                .HasMaxLength(1000)
                .HasColumnName("description");

            builder.Property(a => a.Price)
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0)
                .HasColumnName("price");

            builder.Property(a => a.IsOptional)
                .HasDefaultValue(true)
                .HasColumnName("is_optional");

            // Relationship
            builder.HasOne(a => a.PoojaMaster)
                .WithMany(p => p.Addons)
                .HasForeignKey(a => a.PoojaId)
                .HasConstraintName("fk_pooja_addon_pooja_master");
        }

    }
}
