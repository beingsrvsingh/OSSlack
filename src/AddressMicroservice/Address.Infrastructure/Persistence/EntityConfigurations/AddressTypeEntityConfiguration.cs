using Address.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Address.Infrastructure.Persistence.EntityConfigurations
{
    public class AddressTypeEntityConfiguration : IEntityTypeConfiguration<AddressType>
    {
        public void Configure(EntityTypeBuilder<AddressType> builder)
        {
            builder.ToTable("address_type");

            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("id");

            builder.Property(t => t.Name).HasColumnName("name").IsRequired();
            builder.Property(t => t.Description).HasColumnName("description");

            builder.HasMany(t => t.Addresses)
               .WithOne(a => a.AddressType)
               .HasForeignKey(a => a.AddressTypeId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }

}