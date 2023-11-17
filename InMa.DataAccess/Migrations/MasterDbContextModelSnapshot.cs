﻿// <auto-generated />
using System;
using InMa.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InMa.DataAccess.Migrations
{
    [DbContext(typeof(MasterDbContext))]
    partial class MasterDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("InMa.DataAccess.Models.Inventory", b =>
                {
                    b.Property<Guid>("StorageUnitId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ItemId")
                        .HasColumnType("uuid");

                    b.Property<long>("AvailableQuantity")
                        .HasColumnType("bigint");

                    b.Property<long>("MaxQuantity")
                        .HasColumnType("bigint");

                    b.HasKey("StorageUnitId", "ItemId");

                    b.HasIndex("ItemId");

                    b.ToTable("Inventories");
                });

            modelBuilder.Entity("InMa.DataAccess.Models.Item", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("InMa.DataAccess.Models.StorageUnit", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("StorageUnits");
                });

            modelBuilder.Entity("InMa.DataAccess.Models.Inventory", b =>
                {
                    b.HasOne("InMa.DataAccess.Models.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InMa.DataAccess.Models.StorageUnit", "StorageUnit")
                        .WithMany()
                        .HasForeignKey("StorageUnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("StorageUnit");
                });
#pragma warning restore 612, 618
        }
    }
}
