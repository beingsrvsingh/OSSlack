using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Temple.Domain.Entities;

namespace Temple.Infrastructure.Persistence.EntityConfigurations
{
    public class TempleMasterConfiguration : IEntityTypeConfiguration<TempleMaster>
    {
        public void Configure(EntityTypeBuilder<TempleMaster> builder)
        {
            builder.ToTable("TempleMaster");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.LocationId)
                .IsRequired()
                .HasColumnName("location_id")
                .HasMaxLength(100);

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(t => t.Description)
                .HasMaxLength(1000);

            builder.Property(t => t.ImageUrl)
                .HasMaxLength(300);

            builder.Property(t => t.IsActive)
                .HasDefaultValue(true);

            builder.Property(t => t.CreatedAt)
                .HasDefaultValueSql("TIMESTAMP(6)");

            // Relationships - optional, if navigation props used
            builder.HasMany(t => t.TemplePoojas)
                .WithOne(tp => tp.TempleMaster)
                .HasForeignKey(tp => tp.TempleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(t => t.Donations)
                .WithOne()
                .HasForeignKey(d => d.TempleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(t => t.Prasads)
                .WithOne()
                .HasForeignKey(p => p.TempleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(t => t.Aartis)
                .WithOne()
                .HasForeignKey(a => a.TempleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(t => t.TempleSchedules)
                .WithOne(ts => ts.TempleMaster)
                .HasForeignKey(ts => ts.TempleMasterId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(t => t.TempleExceptions)
                .WithOne(te => te.TempleMaster)
                .HasForeignKey(te => te.TempleMasterId)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }

}