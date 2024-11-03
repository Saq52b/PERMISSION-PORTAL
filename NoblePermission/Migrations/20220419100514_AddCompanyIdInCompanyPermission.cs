using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NoblePermission.Migrations
{
    public partial class AddCompanyIdInCompanyPermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "CompanyPermissions",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5f8d5614-2c7e-4ec0-868c-d254e6516b4d"),
                column: "Password",
                value: "$2a$11$F3WfYbpLzNbH.Hm/UqjsJOYRITAmk32t3KDSU.3Sr572upmjQRjbO");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyPermissions_CompanyId",
                table: "CompanyPermissions",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyPermissions_Companies_CompanyId",
                table: "CompanyPermissions",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyPermissions_Companies_CompanyId",
                table: "CompanyPermissions");

            migrationBuilder.DropIndex(
                name: "IX_CompanyPermissions_CompanyId",
                table: "CompanyPermissions");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "CompanyPermissions");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5f8d5614-2c7e-4ec0-868c-d254e6516b4d"),
                column: "Password",
                value: "$2a$11$jcxpPJCKVAtmhNyeFO0t5.kuK56TYzdvaCO1Oyv5dSaWJXxI4J1mK");
        }
    }
}
