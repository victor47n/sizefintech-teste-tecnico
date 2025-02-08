﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SizeFintech.Infrastructure.DataAccess;

#nullable disable

namespace SizeFintech.Infrastructure.Migrations
{
    [DbContext(typeof(SizeFintechDbContext))]
    [Migration("20250207200038_InitialMigrations")]
    partial class InitialMigrations
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SizeFintech.Domain.Entities.Anticipation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("GrossTotal")
                        .HasPrecision(12, 2)
                        .HasColumnType("decimal(12,2)");

                    b.Property<decimal>("Limit")
                        .HasPrecision(12, 2)
                        .HasColumnType("decimal(12,2)");

                    b.Property<decimal>("NetTotal")
                        .HasPrecision(12, 2)
                        .HasColumnType("decimal(12,2)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Anticipations");
                });

            modelBuilder.Entity("SizeFintech.Domain.Entities.AnticipationLimit", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<decimal>("AnticipationPercent")
                        .HasPrecision(5, 4)
                        .HasColumnType("decimal(5,4)");

                    b.Property<long>("IndustryId")
                        .HasColumnType("bigint");

                    b.Property<decimal?>("RevenueMaximum")
                        .HasPrecision(12, 2)
                        .HasColumnType("decimal(12,2)");

                    b.Property<decimal>("RevenueMinimun")
                        .HasPrecision(12, 2)
                        .HasColumnType("decimal(12,2)");

                    b.HasKey("Id");

                    b.HasIndex("IndustryId");

                    b.ToTable("AnticipationLimits");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            AnticipationPercent = 0.5m,
                            IndustryId = 1L,
                            RevenueMaximum = 50000m,
                            RevenueMinimun = 10000m
                        },
                        new
                        {
                            Id = 2L,
                            AnticipationPercent = 0.5m,
                            IndustryId = 2L,
                            RevenueMaximum = 50000m,
                            RevenueMinimun = 10000m
                        },
                        new
                        {
                            Id = 3L,
                            AnticipationPercent = 0.55m,
                            IndustryId = 1L,
                            RevenueMaximum = 100000m,
                            RevenueMinimun = 50001m
                        },
                        new
                        {
                            Id = 4L,
                            AnticipationPercent = 0.6m,
                            IndustryId = 2L,
                            RevenueMaximum = 100000m,
                            RevenueMinimun = 50001m
                        },
                        new
                        {
                            Id = 5L,
                            AnticipationPercent = 0.6m,
                            IndustryId = 1L,
                            RevenueMinimun = 100001m
                        },
                        new
                        {
                            Id = 6L,
                            AnticipationPercent = 0.65m,
                            IndustryId = 2L,
                            RevenueMinimun = 100001m
                        });
                });

            modelBuilder.Entity("SizeFintech.Domain.Entities.Industry", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Industries");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Name = "Serviços"
                        },
                        new
                        {
                            Id = 2L,
                            Name = "Produtos"
                        });
                });

            modelBuilder.Entity("SizeFintech.Domain.Entities.Invoice", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("AnticipationId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("GrossAmount")
                        .HasPrecision(12, 2)
                        .HasColumnType("decimal(12,2)");

                    b.Property<decimal>("NetAmount")
                        .HasPrecision(12, 2)
                        .HasColumnType("decimal(12,2)");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AnticipationId");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("SizeFintech.Domain.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("CNPJ")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("IndustryId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("MonthlyRevenue")
                        .HasPrecision(12, 2)
                        .HasColumnType("decimal(12,2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserIdentifier")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("IndustryId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SizeFintech.Domain.Entities.Anticipation", b =>
                {
                    b.HasOne("SizeFintech.Domain.Entities.User", "User")
                        .WithMany("Anticipations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SizeFintech.Domain.Entities.AnticipationLimit", b =>
                {
                    b.HasOne("SizeFintech.Domain.Entities.Industry", "Industry")
                        .WithMany("AnticipationLimits")
                        .HasForeignKey("IndustryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Industry");
                });

            modelBuilder.Entity("SizeFintech.Domain.Entities.Invoice", b =>
                {
                    b.HasOne("SizeFintech.Domain.Entities.Anticipation", "Anticipation")
                        .WithMany("Invoices")
                        .HasForeignKey("AnticipationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Anticipation");
                });

            modelBuilder.Entity("SizeFintech.Domain.Entities.User", b =>
                {
                    b.HasOne("SizeFintech.Domain.Entities.Industry", "Industry")
                        .WithMany()
                        .HasForeignKey("IndustryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Industry");
                });

            modelBuilder.Entity("SizeFintech.Domain.Entities.Anticipation", b =>
                {
                    b.Navigation("Invoices");
                });

            modelBuilder.Entity("SizeFintech.Domain.Entities.Industry", b =>
                {
                    b.Navigation("AnticipationLimits");
                });

            modelBuilder.Entity("SizeFintech.Domain.Entities.User", b =>
                {
                    b.Navigation("Anticipations");
                });
#pragma warning restore 612, 618
        }
    }
}
