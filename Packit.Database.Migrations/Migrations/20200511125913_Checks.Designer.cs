﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Packit.DataAccess;

namespace Packit.DataAccess.Migrations
{
    [DbContext(typeof(PackitContext))]
    [Migration("20200511125913_Checks")]
    partial class Checks
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Packit.Model.Backpack", b =>
                {
                    b.Property<int>("BackpackId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("ImageStringName");

                    b.Property<bool>("IsShared");

                    b.Property<string>("Title");

                    b.Property<int>("UserId");

                    b.HasKey("BackpackId");

                    b.HasIndex("UserId");

                    b.ToTable("Backpacks");
                });

            modelBuilder.Entity("Packit.Model.BackpackTrip", b =>
                {
                    b.Property<int>("BackpackId");

                    b.Property<int>("TripId");

                    b.HasKey("BackpackId", "TripId");

                    b.HasIndex("TripId");

                    b.ToTable("BackpackTrip");
                });

            modelBuilder.Entity("Packit.Model.Item", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("ImageStringName");

                    b.Property<bool>("IsChecked");

                    b.Property<string>("Title");

                    b.Property<int>("UserId");

                    b.HasKey("ItemId");

                    b.HasIndex("UserId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("Packit.Model.ItemBackpack", b =>
                {
                    b.Property<int>("ItemId");

                    b.Property<int>("BackpackId");

                    b.HasKey("ItemId", "BackpackId");

                    b.HasIndex("BackpackId");

                    b.ToTable("ItemBackpack");
                });

            modelBuilder.Entity("Packit.Model.Models.Check", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BackpackId");

                    b.Property<bool>("IsChecked");

                    b.Property<int>("ItemId");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.ToTable("Checks");
                });

            modelBuilder.Entity("Packit.Model.Trip", b =>
                {
                    b.Property<int>("TripId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DepatureDate");

                    b.Property<string>("Description");

                    b.Property<string>("Destination");

                    b.Property<string>("ImageStringName");

                    b.Property<string>("Title");

                    b.Property<int>("UserId");

                    b.HasKey("TripId");

                    b.HasIndex("UserId");

                    b.ToTable("Trips");
                });

            modelBuilder.Entity("Packit.Model.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("HashedPassword");

                    b.Property<string>("LastName");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Packit.Model.Backpack", b =>
                {
                    b.HasOne("Packit.Model.User")
                        .WithMany("Backpacks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Packit.Model.BackpackTrip", b =>
                {
                    b.HasOne("Packit.Model.Backpack", "Backpack")
                        .WithMany("Trips")
                        .HasForeignKey("BackpackId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Packit.Model.Trip", "Trip")
                        .WithMany("Backpacks")
                        .HasForeignKey("TripId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Packit.Model.Item", b =>
                {
                    b.HasOne("Packit.Model.User")
                        .WithMany("Items")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Packit.Model.ItemBackpack", b =>
                {
                    b.HasOne("Packit.Model.Backpack", "Backpack")
                        .WithMany("Items")
                        .HasForeignKey("BackpackId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Packit.Model.Item", "Item")
                        .WithMany("Backpacks")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Packit.Model.Models.Check", b =>
                {
                    b.HasOne("Packit.Model.Item")
                        .WithMany("Checks")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Packit.Model.Trip", b =>
                {
                    b.HasOne("Packit.Model.User")
                        .WithMany("Trips")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
