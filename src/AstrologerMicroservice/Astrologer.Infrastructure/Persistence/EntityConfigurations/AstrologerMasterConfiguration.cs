using AstrologerMicroservice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AstrologerMicroservice.Infrastructure.Persistence.EntityConfigurations
{
    public class AstrologerMasterConfiguration : IEntityTypeConfiguration<AstrologerMaster>
    {
        public void Configure(EntityTypeBuilder<AstrologerMaster> builder)
        {
            builder.ToTable("astrologer_master");

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

            // Relationships

            builder.HasMany(a => a.AstrologerLanguages)
                   .WithOne(al => al.Astrologer)
                   .HasForeignKey(al => al.AstrologerId)
                   .HasConstraintName("fk_astrologer_language_astrologer_id")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(a => a.AstrologerExpertises)
                   .WithOne(al => al.Astrologer)
                   .HasForeignKey(al => al.AstrologerId)
                   .HasConstraintName("fk_astrologer_expertise_astrologer_id")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(a => a.Schedules)
                   .WithOne(al => al.Astrologer)
                   .HasForeignKey(al => al.AstrologerId)
                   .HasConstraintName("fk_astrologer_schedules_astrologer_id")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.AttributeValues)
                .WithOne(s => s.AstrologerMaster)
                .HasForeignKey(t => t.AstrologerId)
                .OnDelete(DeleteBehavior.Cascade);  

            builder.HasMany(a => a.AstrologerAddons)
                   .WithOne(aa => aa.Astrologer)
                   .HasForeignKey(aa => aa.AstrologerId)
                   .HasConstraintName("fk_astrologer_addons_astrologer_id")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(a => a.AstrologerMedia)
                   .WithOne(am => am.Astrologer)
                   .HasForeignKey(am => am.AstrologerId)
                   .HasConstraintName("fk_astrologer_media_astrologer_id")
                   .OnDelete(DeleteBehavior.Cascade);

        }

    }
}