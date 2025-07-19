using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class AspNetUserMembershipConfiguration : IEntityTypeConfiguration<AspNetUserMembership>
{
    public void Configure(EntityTypeBuilder<AspNetUserMembership> builder)
    {
        builder.ToTable("aspnet_user_membership");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .HasColumnName("id")
               .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
               .IsRequired()
               .HasMaxLength(450)
               .HasColumnName("user_id");

        builder.Property(x => x.MembershipType)
               .IsRequired()
               .HasColumnName("membership_type");

        builder.Property(x => x.StartDate)
               .HasColumnName("start_date");

        builder.Property(x => x.EndDate)
               .HasColumnName("end_date");

              builder.HasOne(m => m.User)
                     .WithOne(u => u.Membership)
                     .HasForeignKey<AspNetUserMembership>(m => m.UserId);
    }
}
