using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Persistence.EntityConfigurations
{
    public class CityMasterEntityTypeConfiguration : IEntityTypeConfiguration<CityMaster>
    {
        public void Configure(EntityTypeBuilder<CityMaster> entity)
        {
            entity.ToTable("city_master");

            entity.HasIndex(e => new { e.Pincode, e.DistrictName }, "IX_city_master_pin_code_district_name");

            entity.Property(e => e.AreaName).HasMaxLength(100);
            entity.Property(e => e.DistrictName).HasMaxLength(100);

            entity.HasOne(d => d.StateMaster).WithMany(p => p.CityMasters)
                .HasForeignKey(d => d.StateMasterId);
        }
    }
}
