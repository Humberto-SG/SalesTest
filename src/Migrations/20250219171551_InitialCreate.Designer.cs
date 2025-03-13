﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SalesTest.Migrations
{
    [DbContext(typeof(SaleDbContext))]
    [Migration("20250219171551_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SalesTest.Entities.Sale", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Branch")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Customer")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsCancelled")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("SaleDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("SaleNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("Sales");
                });

            modelBuilder.Entity("SalesTest.Entities.Sale", b =>
                {
                    b.OwnsMany("SalesTest.Entities.SaleItem", "Items", b1 =>
                        {
                            b1.Property<Guid>("SaleId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b1.Property<int>("Id"));

                            b1.Property<decimal>("Discount")
                                .HasColumnType("numeric");

                            b1.Property<int>("ProductId")
                                .HasColumnType("integer");

                            b1.Property<int>("Quantity")
                                .HasColumnType("integer");

                            b1.Property<decimal>("UnitPrice")
                                .HasColumnType("numeric");

                            b1.HasKey("SaleId", "Id");

                            b1.ToTable("SaleItem");

                            b1.WithOwner()
                                .HasForeignKey("SaleId");
                        });

                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
