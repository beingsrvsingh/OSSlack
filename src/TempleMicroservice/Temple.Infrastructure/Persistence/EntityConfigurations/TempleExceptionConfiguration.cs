using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Temple.Domain.Entities;


namespace Temple.Infrastructure.Persistence.EntityConfigurations
{
    public class TempleExceptionConfiguration : IEntityTypeConfiguration<TempleException>
    {
        public void Configure(EntityTypeBuilder<TempleException> builder)
        {
            builder.ToTable("TempleException");

            builder.HasKey(te => te.Id);

            builder.Property(te => te.TempleMasterId)
                   .IsRequired();

            builder.Property(te => te.ExceptionDate)
                   .IsRequired();

            builder.Property(te => te.IsClosed)
                   .IsRequired();

            builder.Property(te => te.OpenTime);

            builder.Property(te => te.CloseTime);

            builder.HasOne(te => te.TempleMaster)
                   .WithMany(tm => tm.TempleExceptions)
                   .HasForeignKey(te => te.TempleMasterId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }

}