﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Packit.DataAccess;

namespace Packit.Database.Migrations.Migrations
{
    [DbContext(typeof(PackitContext))]
    [Migration("20200416162903_UserTest2")]
    partial class UserTest2
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

                    b.Property<string>("Title")
                        .IsRequired();

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

                    b.Property<string>("Title")
                        .IsRequired();

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

            modelBuilder.Entity("Packit.Model.SharedBackpack", b =>
                {
                    b.Property<int>("SharedBackpackId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BackpackId");

                    b.Property<int>("UserId");

                    b.HasKey("SharedBackpackId");

                    b.HasIndex("BackpackId")
                        .IsUnique();

                    b.ToTable("SharedBackpacks");
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

                    b.Property<string>("Title")
                        .IsRequired();

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

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("HashedPassword")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Packit.Model.Backpack", b =>
                {
                    b.HasOne("Packit.Model.User")
                        .WithMany("Backpacks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
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
                        .OnDelete(DeleteBehavior.Cascade);
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

            modelBuilder.Entity("Packit.Model.SharedBackpack", b =>
                {
                    b.HasOne("Packit.Model.Backpack", "Backpack")
                        .WithOne("SharedBackpack")
                        .HasForeignKey("Packit.Model.SharedBackpack", "BackpackId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Packit.Model.Trip", b =>
                {
                    b.HasOne("Packit.Model.User")
                        .WithMany("Trips")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
