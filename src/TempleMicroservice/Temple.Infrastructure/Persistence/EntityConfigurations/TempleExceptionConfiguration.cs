using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Temple.Domain.Entities;


namespace Temple.Infrastructure.Persistence.EntityConfigurations
{
    public class TempleExceptionConfiguration : IEntityTypeConfiguration<TempleException>
    {
        public void Configure(EntityTypeBuilder<TempleException> builder)
        {
            builder.ToTable("temple_exception");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id");

            builder.Property(e => e.TempleMasterId)
                .IsRequired()
                .HasColumnName("temple_id");

            builder.Property(e => e.ExceptionDate)
                .IsRequired()
                .HasColumnName("exception_date");

            builder.Property(e => e.IsClosed)
                .IsRequired()
                .HasDefaultValue(false)
                .HasColumnName("is_closed");

            builder.Property(e => e.OpenTime)
                .HasColumnName("open_time");

            builder.Property(e => e.CloseTime)
                .HasColumnName("close_time");

            builder.Property(e => e.Reason)
                .HasMaxLength(500)
                .HasColumnName("reason");

            builder.HasOne(e => e.TempleMaster)
                .WithMany(t => t.TempleExceptions)
                .HasForeignKey(e => e.TempleMasterId)
                .HasConstraintName("fk_temple_exception_temple_master_id")
                .OnDelete(DeleteBehavior.Cascade);
        }

    }

}