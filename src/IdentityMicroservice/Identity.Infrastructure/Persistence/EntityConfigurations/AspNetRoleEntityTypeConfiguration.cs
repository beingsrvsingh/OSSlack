using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Persistence.EntityConfigurations
{
    internal class AspNetRoleEntityTypeConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        //commented once it's migrate
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            //    builder.HasData(
            //new IdentityRole
            //{
            //    Name = "Customer",
            //    NormalizedName = "CUSTOMER"
            //},
            //new IdentityRole
            //{
            //    Name = "Administrator",
            //    NormalizedName = "ADMINISTRATOR"
            //});
        }
    }
}
