using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PriestMicroservice.Domain.Entities;

namespace Priest.Infrastructure.Persistence.EntityConfigurations
{
    public class PriestMasterConfiguration : IEntityTypeConfiguration<PriestMaster>
    {
        public void Configure(EntityTypeBuilder<PriestMaster> builder)
        {
            builder.ToTable("priest_master");

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

            builder.HasMany(a => a.PriestLanguages)
                   .WithOne(al => al.Priest)
                   .HasForeignKey(al => al.PriestId)
                   .HasConstraintName("fk_priest_language_priest_id")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(a => a.PriestExpertise)
                   .WithOne(al => al.PriestMaster)
                   .HasForeignKey(al => al.PriestId)
                   .HasConstraintName("fk_priest_expertise_priest_id")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(a => a.Schedules)
                   .WithOne(al => al.Priest)
                   .HasForeignKey(al => al.PriestId)
                   .HasConstraintName("fk_priest_schedules_priest_id")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.AttributeValues)
                .WithOne(s => s.PriestMaster)
                .HasForeignKey(t => t.PriestId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(a => a.Addons)
                   .WithOne(aa => aa.PriestMaster)
                   .HasForeignKey(aa => aa.PriestId)
                   .HasConstraintName("fk_priest_addons_priest_id")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(a => a.AstrologerMedia)
                   .WithOne(am => am.PriestMaster)
                   .HasForeignKey(am => am.PriestId)
                   .HasConstraintName("fk_priest_media_priest_id")
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}