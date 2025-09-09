using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Temple.Domain.Entities;

namespace Temple.Infrastructure.Persistence.EntityConfigurations
{
    public class TemplePoojaConfiguration : IEntityTypeConfiguration<TemplePooja>
    {
        public void Configure(EntityTypeBuilder<TemplePooja> builder)
        {
            builder.ToTable("TemplePooja");

            builder.HasKey(tp => tp.Id);

            builder.Property(tp => tp.TempleId)
                .IsRequired();

            builder.Property(tp => tp.PoojaMasterId)
                .IsRequired();

            builder.Property(tp => tp.Price)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(tp => tp.IsActive)
                .HasDefaultValue(true);

            builder.Property(tp => tp.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
        }
    }

}