﻿// <auto-generated />
using Catalog.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Catalog.Infrastructure.Migrations
{
    [DbContext(typeof(CatalogDbContext))]
    [Migration("20240217191514_Initial-Create")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Catalog.Domain.Entities.CategoryMaster", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("CategoryMaster", (string)null);
                });

            modelBuilder.Entity("Catalog.Domain.Entities.ChildSubCategoryMaster", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("SubCategoryId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SubCategoryId");

                    b.ToTable("ChildSubCategoryMaster", (string)null);
                });

            modelBuilder.Entity("Catalog.Domain.Entities.SubCategoryMaster", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("SubCategoryMaster", (string)null);
                });

            modelBuilder.Entity("Catalog.Domain.Entities.ChildSubCategoryMaster", b =>
                {
                    b.HasOne("Catalog.Domain.Entities.SubCategoryMaster", "SubCategory")
                        .WithMany("ChildSubCategoryMasters")
                        .HasForeignKey("SubCategoryId")
                        .IsRequired()
                        .HasConstraintName("FK_ChildSubCategoryMaster_SubCategoryMaster");

                    b.Navigation("SubCategory");
                });

            modelBuilder.Entity("Catalog.Domain.Entities.SubCategoryMaster", b =>
                {
                    b.HasOne("Catalog.Domain.Entities.CategoryMaster", "Category")
                        .WithMany("SubCategoryMasters")
                        .HasForeignKey("CategoryId")
                        .IsRequired()
                        .HasConstraintName("FK_SubCategoryMaster_CategoryMaster");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Catalog.Domain.Entities.CategoryMaster", b =>
                {
                    b.Navigation("SubCategoryMasters");
                });

            modelBuilder.Entity("Catalog.Domain.Entities.SubCategoryMaster", b =>
                {
                    b.Navigation("ChildSubCategoryMasters");
                });
#pragma warning restore 612, 618
        }
    }
}
