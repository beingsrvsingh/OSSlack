﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Review.Infrastructure.Persistence.Context;

#nullable disable

namespace Review.Infrastructure.Migrations
{
    [DbContext(typeof(ReviewDbContext))]
    partial class ReviewDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Review.Domain.Entities.ReviewAuditLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("NewValues")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OldValues")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TableId")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("TableName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("TableId");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("TableId"), false);

                    b.HasIndex("TableName");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("TableName"), false);

                    b.HasIndex("UserId");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("UserId"), false);

                    b.ToTable("ReviewAuditLogs");
                });

            modelBuilder.Entity("Review.Domain.Entities.ReviewDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsHelpful")
                        .HasColumnType("bit")
                        .HasColumnName("isHelpful");

                    b.Property<string>("Message")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("ReviewId")
                        .HasColumnType("int");

                    b.Property<int?>("ReviewReportLookUpId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id")
                        .HasName("PK_ReviewDetails");

                    b.HasIndex(new[] { "ReviewReportLookUpId" }, "IX_ReviewDetail_ReportTypeId");

                    b.HasIndex(new[] { "ReviewId" }, "IX_ReviewDetail_ReviewId");

                    b.ToTable("ReviewDetails");
                });

            modelBuilder.Entity("Review.Domain.Entities.ReviewReportLookup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descriptions")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id")
                        .HasName("PK_ReviewReportLookups");

                    b.ToTable("ReviewReportLookup", (string)null);
                });

            modelBuilder.Entity("Review.Domain.Entities.Reviews", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsReviewed")
                        .HasColumnType("bit");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("ReviewReason")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ReviewedBy")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("ReviewedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("ShortDescription")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Star")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id")
                        .HasName("PK_dbo.Reviews");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("Review.Domain.Entities.ReviewDetail", b =>
                {
                    b.HasOne("Review.Domain.Entities.Reviews", "Review")
                        .WithMany("ReviewDetails")
                        .HasForeignKey("ReviewId")
                        .IsRequired()
                        .HasConstraintName("FK_ReviewDetail_Reviews");

                    b.HasOne("Review.Domain.Entities.ReviewReportLookup", "ReviewReportLookUp")
                        .WithMany("ReviewDetails")
                        .HasForeignKey("ReviewReportLookUpId")
                        .HasConstraintName("FK_ReviewDetail_ReviewReportLookup");

                    b.Navigation("Review");

                    b.Navigation("ReviewReportLookUp");
                });

            modelBuilder.Entity("Review.Domain.Entities.ReviewReportLookup", b =>
                {
                    b.Navigation("ReviewDetails");
                });

            modelBuilder.Entity("Review.Domain.Entities.Reviews", b =>
                {
                    b.Navigation("ReviewDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
