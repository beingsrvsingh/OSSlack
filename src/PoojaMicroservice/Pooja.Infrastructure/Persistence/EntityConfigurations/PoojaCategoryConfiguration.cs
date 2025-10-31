using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pooja.Domain.Entities;

namespace Pooja.Infrastructure.Persistence.EntityConfigurations
{
    public class PoojaCategoryConfiguration : IEntityTypeConfiguration<PoojaCategory>
    {
        public void Configure(EntityTypeBuilder<PoojaCategory> builder)
        {            
            builder.ToTable("pooja_category");

            // Primary key
            builder.HasKey(c => c.Id)
                   .HasName("pk_pooja_category");

            builder.Property(c => c.Id)
                .HasColumnName("id");

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnName("name");

            builder.Property(c => c.Description)
                .HasMaxLength(1000)
                .HasColumnName("description");

            builder.Property(c => c.ImageUrl)
                .HasMaxLength(500)
                .HasColumnName("image_url");

            // Relationship with PoojaMaster
            builder.HasMany(c => c.PoojaMasters)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId)
                .HasConstraintName("fk_pooja_master_category");
        }

    }

}
