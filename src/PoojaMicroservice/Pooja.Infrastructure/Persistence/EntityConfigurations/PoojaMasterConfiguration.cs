using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pooja.Domain.Entities;

namespace Pooja.Infrastructure.Persistence.EntityConfigurations
{
    public class PoojaMasterConfiguration : IEntityTypeConfiguration<PoojaMaster>
    {
        public void Configure(EntityTypeBuilder<PoojaMaster> builder)
        {            
            builder.ToTable("pooja_master");

            // Primary key
            builder.HasKey(p => p.Id)
                   .HasName("pk_pooja_master");
            
            builder.Property(p => p.Id)
                .HasColumnName("id");

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("name");

            builder.Property(p => p.Description)
                .HasMaxLength(2000)
                .HasColumnName("description");

            builder.Property(p => p.IsPriceVariable)
                .HasDefaultValue(false)
                .HasColumnName("is_price_variable");

            builder.Property(p => p.BasePrice)
                .HasColumnType("decimal(18,2)")
                .HasColumnName("base_price");

            builder.Property(p => p.DiscountedPrice)
                .HasColumnType("decimal(18,2)")
                .HasColumnName("discounted_price");

            builder.Property(p => p.Duration)
                .IsRequired()
                .HasColumnName("duration");

            builder.Property(p => p.PreparationTime)
                .HasColumnName("preparation_time");

            builder.Property(p => p.IsAvailableOnline)
                .HasDefaultValue(false)
                .HasColumnName("is_available_online");

            builder.Property(p => p.IsTempleRequired)
                .HasDefaultValue(false)
                .HasColumnName("is_temple_required");

            builder.Property(p => p.IsHomeAvailable)
                .HasDefaultValue(false)
                .HasColumnName("is_home_available");

            builder.Property(p => p.PriestIncluded)
                .HasDefaultValue(false)
                .HasColumnName("priest_included");

            builder.Property(p => p.BringYourOwnPriestAllowed)
                .HasDefaultValue(false)
                .HasColumnName("bring_your_own_priest_allowed");

            builder.Property(p => p.AverageRating)
                .HasColumnType("decimal(3,2)")
                .HasDefaultValue(0)
                .HasColumnName("average_rating");

            builder.Property(p => p.TotalReviews)
                .HasDefaultValue(0)
                .HasColumnName("total_reviews");

            builder.Property(p => p.ImageUrl)
                .HasColumnName("image_url");

            builder.Property(p => p.Tags)
                .HasColumnName("tags");

            builder.Property(p => p.CategoryId)
                .HasColumnName("category_id");

            builder.Property(p => p.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                .HasColumnName("created_at");

            builder.Property(p => p.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                .HasColumnName("updated_at");

            builder.Property(p => p.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");

            // Relationships
            builder.HasOne(p => p.Category)
                .WithMany(c => c.PoojaMasters)
                .HasForeignKey(p => p.CategoryId)
                .HasConstraintName("fk_pooja_master_category")
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(p => p.PoojaTemples)
                .WithOne(pt => pt.PoojaMaster)
                .HasForeignKey(pt => pt.PoojaId)
                .HasConstraintName("fk_pooja_temple_pooja_master");

            builder.HasMany(p => p.PoojaPriests)
                .WithOne(pp => pp.PoojaMaster)
                .HasForeignKey(pp => pp.PoojaId)
                .HasConstraintName("fk_pooja_priest_pooja_master");

            builder.HasMany(p => p.Addons)
                .WithOne(a => a.PoojaMaster)
                .HasForeignKey(a => a.PoojaId)
                .HasConstraintName("fk_pooja_addon_pooja_master");
        }

    }
}
