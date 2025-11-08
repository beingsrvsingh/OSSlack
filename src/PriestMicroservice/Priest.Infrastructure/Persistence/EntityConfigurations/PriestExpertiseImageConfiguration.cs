using Priest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Priest.Infrastructure.Persistence.EntityConfigurations
{
    public class PriestExpertiseImageConfiguration : IEntityTypeConfiguration<PriestExpertiseMedia>
    {
        public void Configure(EntityTypeBuilder<PriestExpertiseMedia> builder)
        {
            builder.ToTable("priest_expertise_image");

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

            builder.Property(vi => vi.PriestExpertiseId)
                .IsRequired()
                .HasColumnName("priest_expertise_id");

            builder.Property(p => p.IsActive).HasColumnName("is_active").HasDefaultValue(true);
            builder.Property(p => p.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
            builder.Property(p => p.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            builder.HasOne(vi => vi.PriestExpertise)
                .WithMany(v => v.PriestExpertiseMedia)
                .HasForeignKey(vi => vi.PriestExpertiseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
