﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SanyaCountryDatabaseImplement;

#nullable disable

namespace SanyaCountryDatabaseImplement.Migrations
{
    [DbContext(typeof(SanyaCountryDatabase))]
    [Migration("20230428205145_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("SanyaCountryDatabaseImplement.Models.Building", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<double>("Square")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Buildings");
                });

            modelBuilder.Entity("SanyaCountryDatabaseImplement.Models.Settlement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Settlements");
                });

            modelBuilder.Entity("SanyaCountryDatabaseImplement.Models.SettlementBuilding", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BuildingId")
                        .HasColumnType("int");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int>("SettlementId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BuildingId");

                    b.HasIndex("SettlementId");

                    b.ToTable("SettlementBuildings");
                });

            modelBuilder.Entity("SanyaCountryDatabaseImplement.Models.SettlementBuilding", b =>
                {
                    b.HasOne("SanyaCountryDatabaseImplement.Models.Building", "Building")
                        .WithMany("SettlementBuildings")
                        .HasForeignKey("BuildingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SanyaCountryDatabaseImplement.Models.Settlement", "Settlement")
                        .WithMany("SettlementBuildings")
                        .HasForeignKey("SettlementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Building");

                    b.Navigation("Settlement");
                });

            modelBuilder.Entity("SanyaCountryDatabaseImplement.Models.Building", b =>
                {
                    b.Navigation("SettlementBuildings");
                });

            modelBuilder.Entity("SanyaCountryDatabaseImplement.Models.Settlement", b =>
                {
                    b.Navigation("SettlementBuildings");
                });
#pragma warning restore 612, 618
        }
    }
}
