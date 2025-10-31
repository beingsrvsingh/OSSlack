using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pooja.Domain.Entities;

namespace Pooja.Infrastructure.Persistence.EntityConfigurations
{
    public class PoojaTempleConfiguration : IEntityTypeConfiguration<PoojaTemple>
    {
        public void Configure(EntityTypeBuilder<PoojaTemple> builder)
        {            
            builder.ToTable("pooja_temple");

            // Primary key
            builder.HasKey(pt => pt.Id)
                   .HasName("pk_pooja_temple");

            builder.Property(pt => pt.Id)
                .HasColumnName("id");

            builder.Property(pt => pt.PoojaId)
                .HasColumnName("pooja_id");

            builder.Property(p => p.TempleId)
                .HasColumnName("temple_id");

            builder.Property(pt => pt.TempleCharges)
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0)
                .HasColumnName("temple_charges");

            builder.Property(pt => pt.PriestIncluded)
                .HasDefaultValue(false)
                .HasColumnName("priest_included");

            builder.Property(pt => pt.IsAvailable)
                .HasDefaultValue(true)
                .HasColumnName("is_available");

            // Relationship with PoojaMaster
            builder.HasOne(pt => pt.PoojaMaster)
                .WithMany(p => p.PoojaTemples)
                .HasForeignKey(pt => pt.PoojaId)
                .HasConstraintName("fk_pooja_temple_pooja_master");
        }

    }
}
