using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Persistence.EntityConfigurations
{
    public class CountryMasterEntityTypeConfiguration : IEntityTypeConfiguration<CountryMaster>
    {
        public void Configure(EntityTypeBuilder<CountryMaster> entity)
        {
            entity.ToTable("country_master");

            entity.HasIndex(e => e.Name, "IX_countery_master_Name");
            entity.HasIndex(e => e.DialCode, "IX_country_master_dial_code"); // Added index on DialCode


            entity.Property(e => e.AlphaTwoCode).HasMaxLength(2);
            entity.Property(e => e.DialCode).HasMaxLength(10);
            entity.Property(e => e.Emoji).HasMaxLength(5);
            entity.Property(e => e.ImageUri).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Unicode).HasMaxLength(20);
        }
    }
}
