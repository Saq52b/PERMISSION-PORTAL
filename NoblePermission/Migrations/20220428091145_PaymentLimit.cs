using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NoblePermission.Migrations
{
    public partial class PaymentLimit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActivationPlatform",
                table: "CompanyLicenses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PaymentLimits",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FromDate = table.Column<DateTime>(nullable: false),
                    ToDate = table.Column<DateTime>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentLimits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentLimits_Companies_CompanyId",
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
                value: "$2a$11$l8hAQMzUV6L.C6mM0wcDTuE4wctmlTow7LzA3mgUaZ/AK7MAi7gmq");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentLimits_CompanyId",
                table: "PaymentLimits",
                column: "CompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentLimits");

            migrationBuilder.DropColumn(
                name: "ActivationPlatform",
                table: "CompanyLicenses");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5f8d5614-2c7e-4ec0-868c-d254e6516b4d"),
                column: "Password",
                value: "$2a$11$uQLR2ys8VElrpjYGg58SBO4xOBY0Y..kPIcg6lJNNUmI61mB4wZkK");
        }
    }
}
