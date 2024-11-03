using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NoblePermission.Migrations
{
    public partial class FtpAccountDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FtpAccountDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Host = table.Column<string>(nullable: true),
                    Port = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    SystemType = table.Column<string>(nullable: true),
                    CompanyId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FtpAccountDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FtpAccountDetails_Companies_CompanyId",
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
                value: "$2a$11$SM4ZxMmwdwiYZ6eAylcMOeVk02X0R/ZSf/M1hOL8FoU93f8wumU82");

            migrationBuilder.CreateIndex(
                name: "IX_FtpAccountDetails_CompanyId",
                table: "FtpAccountDetails",
                column: "CompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FtpAccountDetails");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5f8d5614-2c7e-4ec0-868c-d254e6516b4d"),
                column: "Password",
                value: "$2a$11$9UzDOEFf0A.VJd4fGCmhyOUVpQLxcJ25ZzsrG0E.vdzTHgyaNx.pa");
        }
    }
}
