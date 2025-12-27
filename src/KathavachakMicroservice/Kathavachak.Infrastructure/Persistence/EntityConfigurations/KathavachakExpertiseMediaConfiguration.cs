using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Kathavachak.Domain.Entities;

namespace Kathavachak.Infrastructure.Persistence.EntityConfigurations
{
    public class KathavachakExpertiseMediaConfiguration : IEntityTypeConfiguration<KathavachakExpertiseMedia>
    {
        public void Configure(EntityTypeBuilder<KathavachakExpertiseMedia> builder)
        {
            builder.ToTable("kathavachak_expertise_image");

            builder.HasKey(vi => vi.Id);

            builder.Property(vi => vi.Id)
                .HasColumnName("id");

            builder.Property(vi => vi.ImageUrl)
                .IsRequired()
                .HasMaxLength(300)
                .HasColumnName("image_url");

            builder.Property(vi => vi.SortOrder)
                .HasColumnName("sort_order");

            builder.Property(vi => vi.MediaType)
                .HasColumnName("media_type")
                .HasConversion<string>();

            builder.Property(vi => vi.AltText)
                .HasMaxLength(50)
                .HasColumnName("alt_text");

            builder.Property(vi => vi.KathavachakExpertiseId)
                .IsRequired()
                .HasColumnName("kathavachak_expertise_id");

            builder.Property(p => p.IsActive).HasColumnName("is_active").HasDefaultValue(true);
            builder.Property(p => p.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
            builder.Property(p => p.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            builder.HasOne(vi => vi.KathavachakExpertise)
                .WithMany(v => v.Medias)
                .HasForeignKey(vi => vi.KathavachakExpertiseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
