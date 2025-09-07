using Kathavachak.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kathavachak.Infrastructure.Persistence.EntityConfigurations
{
    public class KathavachakMediaConfiguration : IEntityTypeConfiguration<KathavachakMedia>
    {
        public void Configure(EntityTypeBuilder<KathavachakMedia> builder)
        {
            builder.ToTable("kathavachak_media");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
                .HasColumnName("id");

            builder.Property(m => m.KathavachakId)
                .HasColumnName("kathavachak_id")
                .IsRequired();

            builder.Property(m => m.MediaType)
                .HasColumnName("media_type")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(m => m.Url)
                .HasColumnName("url")
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(m => m.Description)
                .HasColumnName("description")
                .HasMaxLength(1000);

            builder.HasOne(m => m.Kathavachak)
                .WithMany(k => k.Media)
                .HasForeignKey(m => m.KathavachakId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
