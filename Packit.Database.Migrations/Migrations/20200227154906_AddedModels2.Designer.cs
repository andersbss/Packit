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
    [Migration("20200227154906_AddedModels2")]
    partial class AddedModels2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Packit.Model.Item", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("ImageFilePath");

                    b.Property<string>("Title");

                    b.Property<int?>("UserId");

                    b.HasKey("ItemId");

                    b.HasIndex("UserId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("Packit.Model.PackingList", b =>
                {
                    b.Property<int>("PackingListId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<int?>("ItemId");

                    b.Property<string>("Title");

                    b.Property<int?>("TripId");

                    b.Property<int?>("UserId");

                    b.HasKey("PackingListId");

                    b.HasIndex("ItemId");

                    b.HasIndex("TripId");

                    b.HasIndex("UserId");

                    b.ToTable("PackingLists");
                });

            modelBuilder.Entity("Packit.Model.SharedPackingList", b =>
                {
                    b.Property<int>("SharedPackingListId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("PackingListId");

                    b.HasKey("SharedPackingListId");

                    b.HasIndex("PackingListId");

                    b.ToTable("SharedPackingLists");
                });

            modelBuilder.Entity("Packit.Model.Trip", b =>
                {
                    b.Property<int>("TripId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DepatureDate");

                    b.Property<string>("Description");

                    b.Property<string>("Destination");

                    b.Property<string>("ImageFileName");

                    b.Property<string>("Title");

                    b.Property<int?>("UserId");

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

                    b.Property<string>("HashedPasword");

                    b.Property<string>("LastName");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Packit.Model.Item", b =>
                {
                    b.HasOne("Packit.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Packit.Model.PackingList", b =>
                {
                    b.HasOne("Packit.Model.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId");

                    b.HasOne("Packit.Model.Trip")
                        .WithMany("PackingLists")
                        .HasForeignKey("TripId");

                    b.HasOne("Packit.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Packit.Model.SharedPackingList", b =>
                {
                    b.HasOne("Packit.Model.PackingList", "PackingList")
                        .WithMany()
                        .HasForeignKey("PackingListId");
                });

            modelBuilder.Entity("Packit.Model.Trip", b =>
                {
                    b.HasOne("Packit.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}