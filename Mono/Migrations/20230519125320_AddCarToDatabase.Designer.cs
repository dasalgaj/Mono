﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Mono.Data;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Mono.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230519125320_AddCarToDatabase")]
    partial class AddCarToDatabase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Mono.Models.VehicleMake", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<string>("abrv")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("VehicleMake");
                });

            modelBuilder.Entity("Mono.Models.VehicleModel", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<string>("abrv")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("make_id")
                        .HasColumnType("integer");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.HasIndex("make_id");

                    b.ToTable("VehicleModel");
                });

            modelBuilder.Entity("Mono.Models.VehicleModel", b =>
                {
                    b.HasOne("Mono.Models.VehicleMake", "VehicleMake")
                        .WithMany()
                        .HasForeignKey("make_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("VehicleMake");
                });
#pragma warning restore 612, 618
        }
    }
}
