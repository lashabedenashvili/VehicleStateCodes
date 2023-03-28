﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VehicleStateCodes.Data.Domein.Domein;

#nullable disable

namespace VehicleStateCodes.Data.Domein.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("VehicleStateCodes.Data.Domein.Data.StateNumber", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreateTime")
                        .IsRequired()
                        .HasColumnType("date");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("nvarchar(7)");

                    b.HasKey("Id");

                    b.ToTable("StateNumber");
                });

            modelBuilder.Entity("VehicleStateCodes.Data.Domein.Data.StateNumberOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("date");

                    b.Property<int>("StateNumberId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StateNumberId");

                    b.ToTable("StateNumberOrder");
                });

            modelBuilder.Entity("VehicleStateCodes.Data.Domein.Data.StateNumberReservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("date");

                    b.Property<DateTime>("EndReservation")
                        .HasColumnType("datetime2");

                    b.Property<int>("StateNumberId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StateNumberId");

                    b.ToTable("StateNumberReservation");
                });

            modelBuilder.Entity("VehicleStateCodes.Data.Domein.Data.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("date");

                    b.Property<int>("Cityzen")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("SurName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("UserRole")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("VehicleStateCodes.Data.Domein.Data.UserPasswordHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("date");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("date");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserPasswordHistory");
                });

            modelBuilder.Entity("VehicleStateCodes.Data.Domein.Data.StateNumberOrder", b =>
                {
                    b.HasOne("VehicleStateCodes.Data.Domein.Data.StateNumber", "StateNumber")
                        .WithMany("StateNumberOrder")
                        .HasForeignKey("StateNumberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StateNumber");
                });

            modelBuilder.Entity("VehicleStateCodes.Data.Domein.Data.StateNumberReservation", b =>
                {
                    b.HasOne("VehicleStateCodes.Data.Domein.Data.StateNumber", "StateNumber")
                        .WithMany("StateNumberReservation")
                        .HasForeignKey("StateNumberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StateNumber");
                });

            modelBuilder.Entity("VehicleStateCodes.Data.Domein.Data.UserPasswordHistory", b =>
                {
                    b.HasOne("VehicleStateCodes.Data.Domein.Data.User", "User")
                        .WithMany("UserPasswordHistory")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("VehicleStateCodes.Data.Domein.Data.StateNumber", b =>
                {
                    b.Navigation("StateNumberOrder");

                    b.Navigation("StateNumberReservation");
                });

            modelBuilder.Entity("VehicleStateCodes.Data.Domein.Data.User", b =>
                {
                    b.Navigation("UserPasswordHistory");
                });
#pragma warning restore 612, 618
        }
    }
}
