using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Entities;

namespace Product.Infrastructure.Persistence.EntityConfigurations
{
    public class LocalizedProductInfoMasterConfiguration : IEntityTypeConfiguration<LocalizedProductInfoMaster>
    {
        public void Configure(EntityTypeBuilder<LocalizedProductInfoMaster> builder)
        {
            builder.ToTable("localized_product_info_master");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.Id)
                .HasColumnName("id");

            builder.Property(l => l.LanguageCode)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnName("language_code");

            builder.Property(l => l.LocalizedName)
                .HasMaxLength(150)
                .HasColumnName("localized_name");

            builder.Property(l => l.LocalizedDescription)
                .IsRequired(false)
                .HasColumnName("localized_description");

            builder.Property(l => l.ProductId)
                .HasColumnName("product_id");

            builder.HasOne(l => l.ProductMaster)
                .WithMany(p => p.LocalizationMasters)
                .HasForeignKey(l => l.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}