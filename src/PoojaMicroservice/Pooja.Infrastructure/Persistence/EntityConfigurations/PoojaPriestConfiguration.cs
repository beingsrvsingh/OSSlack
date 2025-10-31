using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pooja.Domain.Entities;

namespace Pooja.Infrastructure.Persistence.EntityConfigurations
{
    public class PoojaPriestConfiguration : IEntityTypeConfiguration<PoojaPriest>
    {
        public void Configure(EntityTypeBuilder<PoojaPriest> builder)
        {            
            builder.ToTable("pooja_priest");

            // Primary key
            builder.HasKey(pp => pp.Id)
                   .HasName("pk_pooja_priest");

            builder.Property(pp => pp.Id)
                .HasColumnName("id");

            builder.Property(pp => pp.PoojaId)
                .HasColumnName("pooja_id");

            builder.Property(p => p.PriestId)
                .HasColumnName("priest_id");

            builder.Property(pp => pp.IsPreferred)
                .HasDefaultValue(false)
                .HasColumnName("is_preferred");

            builder.Property(pp => pp.CustomCharges)
                .HasColumnType("decimal(18,2)")
                .HasColumnName("custom_charges");            

            builder.Property(pp => pp.IsAvailable)
                .HasDefaultValue(true)
                .HasColumnName("is_available");

            // Relationship with PoojaMaster
            builder.HasOne(pp => pp.PoojaMaster)
                .WithMany(p => p.PoojaPriests)
                .HasForeignKey(pp => pp.PoojaId)
                .HasConstraintName("fk_pooja_priest_pooja_master");
        }

    }
}
