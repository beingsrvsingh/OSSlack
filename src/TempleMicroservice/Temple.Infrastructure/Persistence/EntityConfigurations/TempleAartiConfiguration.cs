
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Temple.Domain.Entities;

namespace Temple.Infrastructure.Persistence.EntityConfigurations
{
    public class TempleAartiConfiguration : IEntityTypeConfiguration<TempleAarti>
    {
        public void Configure(EntityTypeBuilder<TempleAarti> builder)
        {
            builder.ToTable("TempleAarti");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.TempleId)
                .IsRequired();

            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.Description)
                .HasMaxLength(500);

            builder.Property(a => a.Price)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(a => a.IsActive)
                .HasDefaultValue(true);

            builder.Property(a => a.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
        }
    }

}