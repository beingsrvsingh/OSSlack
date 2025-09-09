using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Temple.Domain.Entities;

namespace Temple.Infrastructure.Persistence.EntityConfigurations
{
    public class TemplePrasadConfiguration : IEntityTypeConfiguration<TemplePrasad>
    {
        public void Configure(EntityTypeBuilder<TemplePrasad> builder)
        {
            builder.ToTable("TemplePrasad");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.TempleId)
                .IsRequired();

            builder.Property(p => p.PrasadMasterId)
                .IsRequired();

            builder.Property(p => p.Price)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(p => p.IsActive)
                .HasDefaultValue(true);

            builder.Property(p => p.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
        }
    }

}