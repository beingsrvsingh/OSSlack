using Address.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Address.Infrastructure.Persistence.EntityConfigurations
{
    public class OwnerTypeEntityConfiguration
    {
        public class OwnerTypeConfiguration : IEntityTypeConfiguration<OwnerType>
        {
            public void Configure(EntityTypeBuilder<OwnerType> builder)
            {
                builder.ToTable("owner_type");

                // Primary Key
                builder.HasKey(ot => ot.Id);

                // Columns with snake_case
                builder.Property(ot => ot.Id)
                       .HasColumnName("id");

                builder.Property(ot => ot.Name)
                       .HasColumnName("name")
                       .IsRequired()
                       .HasMaxLength(50);

                builder.Property(ot => ot.DisplayName)
                       .HasColumnName("display_name")
                       .IsRequired()
                       .HasMaxLength(50);

                builder.Property(ot => ot.Description)
                       .HasColumnName("description")
                       .HasMaxLength(200);

                builder.Property(ot => ot.IsActive)
                       .HasColumnName("is_active")
                       .HasDefaultValue(true);

                builder.HasMany(ot => ot.Addresses)
                   .WithOne(a => a.OwnerType)
                   .HasForeignKey(a => a.OwnerTypeId)
                   .OnDelete(DeleteBehavior.Restrict);

                builder.HasData(
                new OwnerType { Id = 1, Name = "User", Description = "Regular user", IsActive = true },
                new OwnerType { Id = 2, Name = "Partner", Description = "Partner account", IsActive = true },
                new OwnerType { Id = 3, Name = "Temple", Description = "Temple account", IsActive = true },
                new OwnerType { Id = 4, Name = "Priest", Description = "Priest account", IsActive = true },
                new OwnerType { Id = 5, Name = "Astrologer", Description = "Astrologer account", IsActive = true },
                new OwnerType { Id = 6, Name = "Vendor", Description = "Vendor account", IsActive = true },
                new OwnerType { Id = 7, Name = "DeliveryPartner", Description = "Delivery partner", IsActive = true },
                new OwnerType { Id = 10, Name = "Other", Description = "Other type", IsActive = true }
                );
            }
        }
    }
}
