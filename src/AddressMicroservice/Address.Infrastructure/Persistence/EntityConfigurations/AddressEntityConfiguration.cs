using Address.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Address.Infrastructure.Persistence.EntityConfigurations
{
    public class AddressEntityConfiguration : IEntityTypeConfiguration<AddressEntity>
    {
        public void Configure(EntityTypeBuilder<AddressEntity> builder)
        {
            builder.ToTable("address");

            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).HasColumnName("id");

            builder.Property(a => a.Uid).HasColumnName("uid").IsRequired();

            builder.Property(a => a.UserId).HasColumnName("user_Id").IsRequired();
            builder.Property(a => a.OwnerTypeId).HasColumnName("owner_type_id").IsRequired();

            builder.Property(a => a.Name).HasColumnName("name");

            builder.Property(a => a.AddressLine1).HasColumnName("address_line_1").IsRequired();
            builder.Property(a => a.AddressLine2).HasColumnName("address_line_2");

            builder.Property(a => a.City).HasColumnName("city").IsRequired();
            builder.Property(a => a.State).HasColumnName("state").IsRequired();
            builder.Property(a => a.Country).HasColumnName("country").IsRequired().HasDefaultValue("India");

            builder.Property(a => a.Pincode).HasColumnName("pincode").IsRequired();

            builder.Property(a => a.Landmark).HasColumnName("landmark");
            builder.Property(a => a.PhoneNumber).IsRequired().HasColumnName("phone_number");

            builder.Property(a => a.IsDefault).HasColumnName("is_default").HasDefaultValue(false);
            builder.Property(a => a.IsActive).HasColumnName("is_active").HasDefaultValue(true);

            builder.Property(a => a.Latitude).HasColumnName("latitude");
            builder.Property(a => a.Longitude).HasColumnName("longitude");

            builder.Property(a => a.TimeZone).HasColumnName("time_zone");

            builder.Property(a => a.CreatedBy).HasColumnName("created_by");
            builder.Property(a => a.UpdatedBy).HasColumnName("updated_by");

            builder.Property(a => a.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
            builder.Property(a => a.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            builder.Property(a => a.AddressTypeId)
                .HasColumnName("address_type_id")
                .HasDefaultValue(1);

            builder.HasOne(a => a.AddressType)
               .WithMany(t => t.Addresses)
               .HasForeignKey(a => a.AddressTypeId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.OwnerType)
               .WithMany(ot => ot.Addresses)
               .HasForeignKey(a => a.OwnerTypeId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(a => new { a.UserId, a.OwnerTypeId, a.IsDefault })
                   .IsUnique()
                   .HasFilter("[IsDefault] = 1");
        }
    }
}