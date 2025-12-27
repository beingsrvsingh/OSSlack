using Kathavachak.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kathavachak.Infrastructure.Persistence.EntityConfigurations
{
    public class KathavachakMediaConfiguration : IEntityTypeConfiguration<KathavachakMedia>
    {
        public void Configure(EntityTypeBuilder<KathavachakMedia> entity)
        {
            entity.ToTable("kathavachak_image");

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

            entity.Property(img => img.KathavachakId)
                .HasColumnName("kathavachak_id")
                .IsRequired();

            entity.Property(vi => vi.MediaType)
                .HasColumnName("media_type")
                .HasConversion<string>();

            entity.Property(p => p.IsActive).HasColumnName("is_active").HasDefaultValue(true);
            entity.Property(p => p.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
            entity.Property(p => p.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            entity.HasOne(img => img.KathavachakMaster)
                .WithMany(p => p.Medias)
                .HasForeignKey(img => img.KathavachakId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
