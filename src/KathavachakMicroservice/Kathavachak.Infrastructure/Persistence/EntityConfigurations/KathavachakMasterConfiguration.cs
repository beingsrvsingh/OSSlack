using Kathavachak.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kathavachak.Infrastructure.Persistence.EntityConfigurations
{   
    public class KathavachakMasterConfiguration : IEntityTypeConfiguration<KathavachakMaster>
    {
        public void Configure(EntityTypeBuilder<KathavachakMaster> builder)
        {
            builder.ToTable("kathavachak_master");

            builder.HasKey(a => a.Id);

            builder.Property(p => p.Id)
                .HasColumnName("id");

            builder.Property(p => p.CategoryId)
                .IsRequired()
                .HasColumnName("category_id");

            builder.Property(p => p.SubCategoryId)
                .IsRequired()
                .HasColumnName("sub_category_id");

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnName("name");

            builder.Property(p => p.ThumbnailUrl)
                .HasMaxLength(300)
                .HasColumnName("thumbnail_url");

            builder.Property(p => p.IsActive)
                .HasMaxLength(50)
                .HasColumnName("is_active");

            builder.Property(p => p.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                .HasColumnName("created_at");

            builder.Property(p => p.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                .HasColumnName("updated_at");

            builder.Property(p => p.Rating)
                .HasColumnName("rating_snap");

            builder.Property(p => p.Reviews)
                .HasColumnName("reviews_snap");

            builder.Property(p => p.CategoryNameSnapshot)
                .HasMaxLength(100)
                .HasColumnName("category_name_snapshot");

            builder.Property(p => p.SubCategoryNameSnapshot)
                .HasMaxLength(100)
                .HasColumnName("sub_category_name_snapshot");

            builder.Property(p => p.IsTrending)
                .HasColumnName("is_trending");

            builder.Property(p => p.IsFeatured)
                .HasColumnName("is_featured");

            builder.Property(p => p.Currency)
                .HasMaxLength(3)
                .HasColumnName("currency");

            // Navigation properties

            builder.HasMany(k => k.KathavachakExpertises)
                .WithOne(e => e.KathavachakMaster)
                .HasForeignKey(e => e.KathavachakId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(k => k.Languages)
                .WithOne(e => e.Kathavachak)
                .HasForeignKey(e => e.KathavachakId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(k => k.Topics)
                .WithOne(e => e.Kathavachak)
                .HasForeignKey(e => e.KathavachakId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(k => k.SessionModes)
                .WithOne(e => e.Kathavachak)
                .HasForeignKey(e => e.KathavachakId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(k => k.Schedules)
                .WithOne(e => e.Kathavachak)
                .HasForeignKey(e => e.KathavachakId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.AttributeValues)
                .WithOne(s => s.KathavachakMaster)
                .HasForeignKey(t => t.KathavachakId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(a => a.KathavachakAddons)
                   .WithOne(aa => aa.KathavachakMaster)
                   .HasForeignKey(aa => aa.KathavachakId)
                   .HasConstraintName("fk_kathavachak_addons_kathavachak_id")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(k => k.KathavachakMedia)
                .WithOne(e => e.KathavachakMaster)
                .HasForeignKey(e => e.KathavachakId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
