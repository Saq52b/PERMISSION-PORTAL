﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NoblePermission.Persistance;

namespace NoblePermission.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220428091145_PaymentLimit")]
    partial class PaymentLimit
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NoblePermission.Persistance.Entities.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AddressArabic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressEnglish")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Blocked")
                        .HasColumnType("bit");

                    b.Property<Guid?>("BusinessParentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CategoryInArabic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CategoryInEnglish")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CityArabic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CityEnglish")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClientNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ClientParentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CompanyEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyNameArabic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyNameEnglish")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyRegNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CountryArabic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CountryEnglish")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("HouseNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Landline")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LogoPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameArabic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameEnglish")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("NobleGroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ParentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PhoneNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Postcode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Town")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VatRegistrationNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Website")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("NobleGroupId");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("NoblePermission.Persistance.Entities.CompanyLicense", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ActivationPlatform")
                        .HasColumnType("int");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("GracePeriod")
                        .HasColumnType("bit");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsBlock")
                        .HasColumnType("bit");

                    b.Property<bool>("IsTechnicalSupport")
                        .HasColumnType("bit");

                    b.Property<bool>("IsUpdateTechnicalSupport")
                        .HasColumnType("bit");

                    b.Property<string>("LicenseType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentFrequency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TechnicalSupportPeriod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ToDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("CompanyLicenses");
                });

            modelBuilder.Entity("NoblePermission.Persistance.Entities.CompanyPermission", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Key")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("NobleGroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("NobleModuleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PermissionName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("NobleGroupId");

                    b.HasIndex("NobleModuleId");

                    b.ToTable("CompanyPermissions");
                });

            modelBuilder.Entity("NoblePermission.Persistance.Entities.NobleGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("GroupName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GroupType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("NobleGroups");
                });

            modelBuilder.Entity("NoblePermission.Persistance.Entities.NobleModule", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("NobleModules");
                });

            modelBuilder.Entity("NoblePermission.Persistance.Entities.NoblePermission", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Key")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("NobleGroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("NobleModuleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PermissionName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("NobleGroupId");

                    b.HasIndex("NobleModuleId");

                    b.ToTable("NoblePermissions");
                });

            modelBuilder.Entity("NoblePermission.Persistance.Entities.PaymentLimit", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ToDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("PaymentLimits");
                });

            modelBuilder.Entity("NoblePermission.Persistance.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("5f8d5614-2c7e-4ec0-868c-d254e6516b4d"),
                            Email = "permission@gmail.com",
                            Password = "$2a$11$l8hAQMzUV6L.C6mM0wcDTuE4wctmlTow7LzA3mgUaZ/AK7MAi7gmq",
                            UserName = "Permission"
                        });
                });

            modelBuilder.Entity("NoblePermission.Persistance.Entities.Company", b =>
                {
                    b.HasOne("NoblePermission.Persistance.Entities.NobleGroup", "NobleGroup")
                        .WithMany("Companies")
                        .HasForeignKey("NobleGroupId");
                });

            modelBuilder.Entity("NoblePermission.Persistance.Entities.CompanyLicense", b =>
                {
                    b.HasOne("NoblePermission.Persistance.Entities.Company", "Company")
                        .WithMany("CompanyLicenses")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NoblePermission.Persistance.Entities.CompanyPermission", b =>
                {
                    b.HasOne("NoblePermission.Persistance.Entities.Company", "Company")
                        .WithMany("CompanyPermissions")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NoblePermission.Persistance.Entities.NobleGroup", "NobleGroup")
                        .WithMany("CompanyPermissions")
                        .HasForeignKey("NobleGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NoblePermission.Persistance.Entities.NobleModule", "NobleModules")
                        .WithMany("CompanyPermissions")
                        .HasForeignKey("NobleModuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NoblePermission.Persistance.Entities.NoblePermission", b =>
                {
                    b.HasOne("NoblePermission.Persistance.Entities.NobleGroup", "NobleGroup")
                        .WithMany("NoblePermissions")
                        .HasForeignKey("NobleGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NoblePermission.Persistance.Entities.NobleModule", "NobleModule")
                        .WithMany("NoblePermissions")
                        .HasForeignKey("NobleModuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NoblePermission.Persistance.Entities.PaymentLimit", b =>
                {
                    b.HasOne("NoblePermission.Persistance.Entities.Company", "Company")
                        .WithMany("PaymentLimits")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
