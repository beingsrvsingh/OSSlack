using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockMicroservice.Domain.Entities;

namespace StockMicroservice.Infrastructure.Persistence.EntityConfigurations
{
    public class WarehouseTransferConfiguration : IEntityTypeConfiguration<WarehouseTransfer>
    {
        public void Configure(EntityTypeBuilder<WarehouseTransfer> builder)
        {
            builder.ToTable("warehouse_transfer");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.FromWarehouseId)
                .IsRequired()
                .HasColumnName("from_warehouse_id");

            builder.Property(e => e.ToWarehouseId)
                .IsRequired()
                .HasColumnName("to_warehouse_id");

            builder.Property(e => e.ProductId)
                .IsRequired()
                .HasColumnName("product_id");

            builder.Property(e => e.Quantity)
                .IsRequired()
                .HasColumnName("quantity");

            builder.Property(e => e.TransferDate)
            .IsRequired()
            .HasColumnName("transfer_date");

            builder.Property(e => e.Notes)
            .IsRequired()
            .HasColumnName("notes");

            builder.Property(e => e.Status)
                .IsRequired()
                .HasColumnName("status")
                .HasConversion<string>()
                .HasMaxLength(50);

            builder.Property(e => e.CreatedAt)
                .IsRequired()
                .HasColumnName("created_at");

            builder.Property(e => e.CompletedAt)
                .HasColumnName("completed_at");

            builder.HasOne(e => e.FromWarehouse)
                .WithMany()
                .HasForeignKey(e => e.FromWarehouseId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_warehouse_transfer_from_warehouse");

            builder.HasOne(e => e.ToWarehouse)
                .WithMany()
                .HasForeignKey(e => e.ToWarehouseId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_warehouse_transfer_to_warehouse");
        }
    }
}