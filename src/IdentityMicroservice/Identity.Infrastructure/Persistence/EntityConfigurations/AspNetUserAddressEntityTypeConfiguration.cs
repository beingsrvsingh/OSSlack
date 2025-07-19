using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Persistence.EntityConfigurations
{
    internal class AspNetUserAddressEntityTypeConfiguration : IEntityTypeConfiguration<AspNetUserAddress>
    {
        public void Configure(EntityTypeBuilder<AspNetUserAddress> entity)
        {
            // Table name
            entity.ToTable("aspnet_user_address");

            // Properties
            entity.Property(e => e.Id)
                  .HasColumnName("id")
                  .ValueGeneratedOnAdd();

            entity.Property(e => e.UserId)
                  .IsRequired()
                  .HasColumnName("user_id");

            entity.Property(e => e.StreetAddress)
                  .IsRequired()
                  .HasMaxLength(255)
                  .HasColumnName("street_address");

            entity.Property(e => e.ApartmentSuitUnitAddress)
                  .HasMaxLength(255)
                  .HasColumnName("apartment_address");

            entity.Property(e => e.City)
                  .IsRequired()
                  .HasMaxLength(100)
                  .HasColumnName("city");

            entity.Property(e => e.State)
                  .IsRequired()
                  .HasMaxLength(100)
                  .HasColumnName("state");

            entity.Property(e => e.Country)
                  .IsRequired()
                  .HasMaxLength(100)
                  .HasColumnName("country");

            entity.Property(e => e.ZipCode)
                  .IsRequired()
                  .HasMaxLength(20)
                  .HasColumnName("zip_code");

            entity.Property(e => e.IsDeleted)
                  .HasColumnName("is_deleted")
                  .HasDefaultValue(false);

            entity.Property(e => e.IsDefault)
                  .HasColumnName("is_default")
                  .HasDefaultValue(false);

            entity.Property(e => e.CreatedAt)
                  .HasColumnName("created_at")
                  .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.Property(e => e.UpdatedAt)
                  .HasColumnName("updated_at");

                  entity.HasOne(e => e.User)
                        .WithMany(e => e.AspNetUserAddresses)
                        .HasForeignKey(e => e.UserId);
        }
    }

}
