using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NoblePermission.Migrations
{
    public partial class WhiteLabeling : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WhiteLabellings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Heading = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    TagImage1Name = table.Column<string>(nullable: true),
                    TagImage1Path = table.Column<string>(nullable: true),
                    TagImage2Path = table.Column<string>(nullable: true),
                    TagImage2Name = table.Column<string>(nullable: true),
                    CompanyName = table.Column<string>(nullable: true),
                    ApplicationName = table.Column<string>(nullable: true),
                    AddressLine1 = table.Column<string>(nullable: true),
                    AddressLine2 = table.Column<string>(nullable: true),
                    AddressLine3 = table.Column<string>(nullable: true),
                    LoginScreenImageName = table.Column<string>(nullable: true),
                    LoginScreenImagePath = table.Column<string>(nullable: true),
                    LoginLogoName = table.Column<string>(nullable: true),
                    LoginLogoPath = table.Column<string>(nullable: true),
                    BackgroundImageName = table.Column<string>(nullable: true),
                    BackgroundImagePath = table.Column<string>(nullable: true),
                    SidebarImageName = table.Column<string>(nullable: true),
                    SidebarImagePath = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    FavIconPath = table.Column<string>(nullable: true),
                    FavIconName = table.Column<string>(nullable: true),
                    FavName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WhiteLabellings", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5f8d5614-2c7e-4ec0-868c-d254e6516b4d"),
                column: "Password",
                value: "$2a$11$9UzDOEFf0A.VJd4fGCmhyOUVpQLxcJ25ZzsrG0E.vdzTHgyaNx.pa");

            migrationBuilder.InsertData(
                table: "WhiteLabellings",
                columns: new[] { "Id", "AddressLine1", "AddressLine2", "AddressLine3", "ApplicationName", "BackgroundImageName", "BackgroundImagePath", "CompanyName", "Description", "Email", "FavIconName", "FavIconPath", "FavName", "Heading", "LoginLogoName", "LoginLogoPath", "LoginScreenImageName", "LoginScreenImagePath", "SidebarImageName", "SidebarImagePath", "TagImage1Name", "TagImage1Path", "TagImage2Name", "TagImage2Path" },
                values: new object[] { new Guid("fd7803dc-8eaa-4a6e-91de-dca86dddabb0"), "", "", "", "NOBLEPOS", null, null, "TechoQode (Pvt) Ltd.", "Handle All your Needs & Automate Business Process", null, null, null, null, "ERP FOR SMALL & MEDIUM ENTERPRISE", null, null, null, null, null, null, null, null, null, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WhiteLabellings");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5f8d5614-2c7e-4ec0-868c-d254e6516b4d"),
                column: "Password",
                value: "$2a$11$l8hAQMzUV6L.C6mM0wcDTuE4wctmlTow7LzA3mgUaZ/AK7MAi7gmq");
        }
    }
}
