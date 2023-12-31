﻿// <auto-generated />
using System;
using GTHRM.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GTHRM.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230705065740_attSummary")]
    partial class attSummary
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GTHRM.Models.Attendance", b =>
                {
                    b.Property<Guid>("ComId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EmpId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("dtDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("appStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("inTime")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("outTime")
                        .HasColumnType("time");

                    b.HasKey("ComId", "EmpId", "dtDate");

                    b.HasIndex("EmpId");

                    b.ToTable("Attendances");
                });

            modelBuilder.Entity("GTHRM.Models.AttendanceSummary", b =>
                {
                    b.Property<Guid>("ComId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EmpId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("year")
                        .HasColumnType("int");

                    b.Property<int>("month")
                        .HasColumnType("int");

                    b.Property<int>("absent")
                        .HasColumnType("int");

                    b.Property<int>("late")
                        .HasColumnType("int");

                    b.Property<int>("present")
                        .HasColumnType("int");

                    b.HasKey("ComId", "EmpId", "year", "month");

                    b.HasIndex("EmpId");

                    b.ToTable("AttendanceSummaries");
                });

            modelBuilder.Entity("GTHRM.Models.Company", b =>
                {
                    b.Property<Guid>("ComId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<decimal>("Basic")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Hrent")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<decimal>("Medical")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Others")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ComId");

                    b.HasIndex("CompanyName")
                        .IsUnique();

                    b.ToTable("Company");
                });

            modelBuilder.Entity("GTHRM.Models.Department", b =>
                {
                    b.Property<Guid>("DeptId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<Guid>("ComId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DepartmentName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("DeptId");

                    b.HasIndex("ComId", "DepartmentName")
                        .IsUnique();

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("GTHRM.Models.Designation", b =>
                {
                    b.Property<Guid>("DesigId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<Guid>("ComId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DesigName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("DesigId");

                    b.HasIndex("ComId", "DesigName")
                        .IsUnique();

                    b.ToTable("Designations");
                });

            modelBuilder.Entity("GTHRM.Models.Employee", b =>
                {
                    b.Property<Guid>("EmpId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<decimal>("Basic")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("ComId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DeptId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DesigId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EmpCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("EmpName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Gross")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Hrent")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Medical")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Others")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("ShiftId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("dtJoin")
                        .HasColumnType("datetime2");

                    b.Property<int>("gender")
                        .HasColumnType("int");

                    b.HasKey("EmpId");

                    b.HasIndex("DeptId");

                    b.HasIndex("DesigId");

                    b.HasIndex("ShiftId");

                    b.HasIndex("ComId", "EmpCode")
                        .IsUnique();

                    b.ToTable("Employees", t =>
                        {
                            t.HasTrigger("calculateBasic");
                        });
                });

            modelBuilder.Entity("GTHRM.Models.Shift", b =>
                {
                    b.Property<Guid>("ShiftId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<Guid>("ComId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ShiftIn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ShiftLate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ShiftName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("ShiftOut")
                        .HasColumnType("datetime2");

                    b.HasKey("ShiftId");

                    b.HasIndex("ComId");

                    b.ToTable("Shifts");
                });

            modelBuilder.Entity("GTHRM.Models.Attendance", b =>
                {
                    b.HasOne("GTHRM.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("ComId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GTHRM.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmpId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("GTHRM.Models.AttendanceSummary", b =>
                {
                    b.HasOne("GTHRM.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("ComId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GTHRM.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmpId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("GTHRM.Models.Department", b =>
                {
                    b.HasOne("GTHRM.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("ComId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("GTHRM.Models.Designation", b =>
                {
                    b.HasOne("GTHRM.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("ComId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("GTHRM.Models.Employee", b =>
                {
                    b.HasOne("GTHRM.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("ComId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GTHRM.Models.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DeptId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GTHRM.Models.Designation", "Designation")
                        .WithMany()
                        .HasForeignKey("DesigId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GTHRM.Models.Shift", "Shift")
                        .WithMany()
                        .HasForeignKey("ShiftId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("Department");

                    b.Navigation("Designation");

                    b.Navigation("Shift");
                });

            modelBuilder.Entity("GTHRM.Models.Shift", b =>
                {
                    b.HasOne("GTHRM.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("ComId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });
#pragma warning restore 612, 618
        }
    }
}
