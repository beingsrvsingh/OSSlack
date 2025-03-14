using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Persistence.EntityConfigurations
{
    public class AspNetUserAddressEntityTypeConfiguration : IEntityTypeConfiguration<AspNetUserAddress>
    {
        public void Configure(EntityTypeBuilder<AspNetUserAddress> entity)
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Address1).HasMaxLength(200);
            entity.Property(e => e.Address2).HasMaxLength(200);
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.Country).HasMaxLength(50);
            entity.Property(e => e.State).HasMaxLength(50);
            entity.HasIndex(e => e.UserId).IsClustered(false);
            entity.Property(e => e.UserId).HasMaxLength(50);
        }
    }
}
