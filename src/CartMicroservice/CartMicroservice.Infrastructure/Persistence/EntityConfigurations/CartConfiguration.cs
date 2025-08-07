using CartMicroservice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CartMicroservice.Infrastructure.Persistence.EntityConfigurations
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("cart");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasColumnName("id");

            builder.Property(c => c.UserId)
                .IsRequired()
                .HasMaxLength(36)
                .HasColumnName("user_id");

            builder.Property(c => c.PlatformFee)
                .HasColumnType("decimal(10,2)")
                .HasDefaultValue(0m)
                .HasColumnName("platform_fee");

            builder.Property(c => c.SurgeFee)
                .HasColumnType("decimal(10,2)")
                .HasDefaultValue(0m)
                .HasColumnName("surge_fee");

            builder.Property(c => c.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_at");

            builder.Property(c => c.UpdatedAt)
                .HasColumnName("updated_at");

            builder.Property(c => c.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");

            builder.HasIndex(c => c.UserId).HasDatabaseName("idx_cart_user_id");
        }

    }

}