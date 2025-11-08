using Priest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Priest.Infrastructure.Persistence.EntityConfigurations
{
    public class PriestImageConfiguration : IEntityTypeConfiguration<PriestMedia>
    {
        public void Configure(EntityTypeBuilder<PriestMedia> entity)
        {
            entity.ToTable("priest_image");

            entity.HasKey(img => img.Id);

            entity.Property(img => img.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.Property(img => img.ImageUrl)
                .HasColumnName("image_url")
                .IsRequired()
                .HasMaxLength(300);

            entity.Property(entity => entity.SortOrder)
                .HasColumnName("sort_order");

            entity.Property(entity => entity.AltText)
                .HasColumnName("alt_text");

            entity.Property(img => img.PriestId)
                .HasColumnName("priest_id")
                .IsRequired();

            entity.Property(vi => vi.MediaType)
                .HasColumnName("media_type")
                .HasConversion<string>();

            entity.Property(p => p.IsActive).HasColumnName("is_active").HasDefaultValue(true);
            entity.Property(p => p.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
            entity.Property(p => p.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            entity.HasOne(img => img.PriestMaster)
                .WithMany(p => p.AstrologerMedia)
                .HasForeignKey(img => img.PriestId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
