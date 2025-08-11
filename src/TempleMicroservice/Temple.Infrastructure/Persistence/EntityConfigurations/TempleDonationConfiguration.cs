using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Temple.Domain.Entities;

namespace Temple.Infrastructure.Persistence.EntityConfigurations
{
    public class TempleDonationConfiguration : IEntityTypeConfiguration<TempleDonation>
    {
        public void Configure(EntityTypeBuilder<TempleDonation> builder)
        {
            builder.ToTable("TempleDonation");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.TempleId)
                .IsRequired();

            builder.Property(d => d.Amount)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(d => d.DonorName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(d => d.Message)
                .HasMaxLength(500);

            builder.Property(d => d.DonationDate)
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }

}