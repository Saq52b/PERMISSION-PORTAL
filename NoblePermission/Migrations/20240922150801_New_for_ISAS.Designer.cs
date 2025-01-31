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
    [Migration("20240922150801_New_for_ISAS")]
    partial class New_for_ISAS
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

            modelBuilder.Entity("NoblePermission.Persistance.Entities.FtpAccountDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Host")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Port")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SystemType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("FtpAccountDetails");
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
                            Password = "$2a$11$DiLKJjXpGAo8rt.EOwGo8O/83Z9WSn3qsa/hxQ66PuBNDOwMATLCe",
                            UserName = "Permission"
                        });
                });

            modelBuilder.Entity("NoblePermission.Persistance.Entities.WhiteLabelling", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AddressLine1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressLine2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressLine3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ApplicationBgColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ApplicationName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ApplicationTextColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BackgroundImageName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BackgroundImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CancelBgBtnColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CancelBtnColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CardBgColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CardTextColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FavIconName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FavIconPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FavName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Heading")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HeadingColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InvoiceTitleBgColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InvoiceTitleColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LoginLogoName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LoginLogoPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LoginScreenImageName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LoginScreenImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SaveBtnBgColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SaveBtnColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SetupBgColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SetupCssFilter")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SetupTextColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SideMenuBtnClickBgColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SideMenuBtnClickColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SideMenuBtnClickColorFilter")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SideMenuBtnColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SideMenuBtnColorFilter")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SideMenuColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SidebarImageName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SidebarImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TableHeaderBgColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TableHeaderColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TagImage1Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TagImage1Path")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TagImage2Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TagImage2Path")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("WhiteLabellings");

                    b.HasData(
                        new
                        {
                            Id = new Guid("fd7803dc-8eaa-4a6e-91de-dca86dddabb0"),
                            AddressLine1 = "",
                            AddressLine2 = "",
                            AddressLine3 = "",
                            ApplicationName = "NOBLEPOS",
                            CompanyName = "TechoQode (Pvt) Ltd.",
                            Description = "Handle All your Needs & Automate Business Process",
                            Heading = "ERP FOR SMALL & MEDIUM ENTERPRISE"
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

            modelBuilder.Entity("NoblePermission.Persistance.Entities.FtpAccountDetail", b =>
                {
                    b.HasOne("NoblePermission.Persistance.Entities.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
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
