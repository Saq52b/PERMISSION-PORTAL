using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NoblePermission.Migrations
{
    public partial class AutoCompanyPermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Blocked = table.Column<bool>(nullable: false),
                    LogoPath = table.Column<string>(nullable: true),
                    HouseNumber = table.Column<string>(nullable: true),
                    CompanyRegNo = table.Column<string>(nullable: true),
                    NameEnglish = table.Column<string>(nullable: true),
                    NameArabic = table.Column<string>(nullable: true),
                    VatRegistrationNo = table.Column<string>(nullable: true),
                    CompanyEmail = table.Column<string>(nullable: true),
                    CityEnglish = table.Column<string>(nullable: true),
                    CityArabic = table.Column<string>(nullable: true),
                    CountryEnglish = table.Column<string>(nullable: true),
                    CountryArabic = table.Column<string>(nullable: true),
                    CategoryInEnglish = table.Column<string>(nullable: true),
                    CategoryInArabic = table.Column<string>(nullable: true),
                    AddressEnglish = table.Column<string>(nullable: true),
                    AddressArabic = table.Column<string>(nullable: true),
                    PhoneNo = table.Column<string>(nullable: true),
                    Landline = table.Column<string>(nullable: true),
                    Website = table.Column<string>(nullable: true),
                    Postcode = table.Column<string>(nullable: true),
                    Town = table.Column<string>(nullable: true),
                    ClientNo = table.Column<string>(nullable: true),
                    ParentId = table.Column<Guid>(nullable: false),
                    ClientParentId = table.Column<Guid>(nullable: true),
                    BusinessParentId = table.Column<Guid>(nullable: true),
                    CompanyNameEnglish = table.Column<string>(nullable: true),
                    CompanyNameArabic = table.Column<string>(nullable: true),
                    NobleGroupId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_NobleGroups_NobleGroupId",
                        column: x => x.NobleGroupId,
                        principalTable: "NobleGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyPermissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PermissionName = table.Column<string>(nullable: true),
                    Key = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    NobleGroupId = table.Column<Guid>(nullable: false),
                    NobleModuleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyPermissions_NobleGroups_NobleGroupId",
                        column: x => x.NobleGroupId,
                        principalTable: "NobleGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyPermissions_NobleModules_NobleModuleId",
                        column: x => x.NobleModuleId,
                        principalTable: "NobleModules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyLicenses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FromDate = table.Column<DateTime>(nullable: false),
                    ToDate = table.Column<DateTime>(nullable: false),
                    IsBlock = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyLicenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyLicenses_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5f8d5614-2c7e-4ec0-868c-d254e6516b4d"),
                column: "Password",
                value: "$2a$11$jcxpPJCKVAtmhNyeFO0t5.kuK56TYzdvaCO1Oyv5dSaWJXxI4J1mK");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_NobleGroupId",
                table: "Companies",
                column: "NobleGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyLicenses_CompanyId",
                table: "CompanyLicenses",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyPermissions_NobleGroupId",
                table: "CompanyPermissions",
                column: "NobleGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyPermissions_NobleModuleId",
                table: "CompanyPermissions",
                column: "NobleModuleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyLicenses");

            migrationBuilder.DropTable(
                name: "CompanyPermissions");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5f8d5614-2c7e-4ec0-868c-d254e6516b4d"),
                column: "Password",
                value: "$2a$11$GAaVfMy6fmzhyIzkMlKy8OXkleFnRnpgUFGtEGj9vYNe2moIWqU2y");
        }
    }
}
