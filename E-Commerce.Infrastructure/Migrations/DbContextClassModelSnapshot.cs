﻿// <auto-generated />
using System;
using E_Commerce.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace E_Commerce.Infrastructure.Migrations
{
    [DbContext(typeof(DbContextClass))]
    partial class DbContextClassModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("E_Commerce.Domain.Model.Category.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<Guid?>("ParentCategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("RootCategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("categories");
                });

            modelBuilder.Entity("E_Commerce.Domain.Model.Product.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("E_Commerce.Domain.Model.RootCategory.RootCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("rootCategories");
                });

            modelBuilder.Entity("E_Commerce.Domain.Model.Category.Category", b =>
                {
                    b.HasOne("E_Commerce.Domain.Model.Category.Category", null)
                        .WithMany("ChildsCategories")
                        .HasForeignKey("CategoryId");
                });

            modelBuilder.Entity("E_Commerce.Domain.Model.Product.Product", b =>
                {
                    b.OwnsOne("E_Commerce.Domain.Model.Product.Discount", "Discount", b1 =>
                        {
                            b1.Property<Guid>("ProductId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("DiscountAmount")
                                .HasColumnType("int");

                            b1.Property<DateTime?>("DiscountEnd")
                                .HasColumnType("datetime2");

                            b1.Property<DateTime?>("DiscountStart")
                                .HasColumnType("datetime2");

                            b1.HasKey("ProductId");

                            b1.ToTable("Products");

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });

                    b.OwnsOne("E_Commerce.Domain.Model.Product.Price", "Price", b1 =>
                        {
                            b1.Property<Guid>("ProductId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<decimal>("Value")
                                .HasColumnType("decimal(18,2)");

                            b1.HasKey("ProductId");

                            b1.ToTable("Products");

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });

                    b.Navigation("Discount")
                        .IsRequired();

                    b.Navigation("Price")
                        .IsRequired();
                });

            modelBuilder.Entity("E_Commerce.Domain.Model.Category.Category", b =>
                {
                    b.Navigation("ChildsCategories");
                });
#pragma warning restore 612, 618
        }
    }
}
