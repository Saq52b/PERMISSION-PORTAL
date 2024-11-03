using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NoblePermission.Migrations
{
    public partial class CompanyLicense : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "GracePeriod",
                table: "CompanyLicenses",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTechnicalSupport",
                table: "CompanyLicenses",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUpdateTechnicalSupport",
                table: "CompanyLicenses",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LicenseType",
                table: "CompanyLicenses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentFrequency",
                table: "CompanyLicenses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TechnicalSupportPeriod",
                table: "CompanyLicenses",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5f8d5614-2c7e-4ec0-868c-d254e6516b4d"),
                column: "Password",
                value: "$2a$11$uQLR2ys8VElrpjYGg58SBO4xOBY0Y..kPIcg6lJNNUmI61mB4wZkK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GracePeriod",
                table: "CompanyLicenses");

            migrationBuilder.DropColumn(
                name: "IsTechnicalSupport",
                table: "CompanyLicenses");

            migrationBuilder.DropColumn(
                name: "IsUpdateTechnicalSupport",
                table: "CompanyLicenses");

            migrationBuilder.DropColumn(
                name: "LicenseType",
                table: "CompanyLicenses");

            migrationBuilder.DropColumn(
                name: "PaymentFrequency",
                table: "CompanyLicenses");

            migrationBuilder.DropColumn(
                name: "TechnicalSupportPeriod",
                table: "CompanyLicenses");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5f8d5614-2c7e-4ec0-868c-d254e6516b4d"),
                column: "Password",
                value: "$2a$11$F3WfYbpLzNbH.Hm/UqjsJOYRITAmk32t3KDSU.3Sr572upmjQRjbO");
        }
    }
}
