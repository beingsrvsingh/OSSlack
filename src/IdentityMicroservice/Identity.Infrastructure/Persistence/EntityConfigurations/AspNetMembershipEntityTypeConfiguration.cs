using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Data.EntityConfigurations
{
    internal class AspNetMembershipEntityTypeConfiguration : IEntityTypeConfiguration<AspNetMembership>
    {
        public void Configure(EntityTypeBuilder<AspNetMembership> entity)
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.MembershipType).HasColumnType("nvarchar(50)");
            entity.HasOne(y => y.Role).WithMany().HasForeignKey(x => x.RoleId).IsRequired();
            entity.Property(x => x.Cost);
        }
    }
}
