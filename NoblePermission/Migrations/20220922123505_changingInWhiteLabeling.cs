using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NoblePermission.Migrations
{
    public partial class changingInWhiteLabeling : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CloseBtnColor",
                table: "WhiteLabellings");

            migrationBuilder.AddColumn<string>(
                name: "CancelBgBtnColor",
                table: "WhiteLabellings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InvoiceTitleBgColor",
                table: "WhiteLabellings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InvoiceTitleColor",
                table: "WhiteLabellings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SaveBtnBgColor",
                table: "WhiteLabellings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TableHeaderBgColor",
                table: "WhiteLabellings",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5f8d5614-2c7e-4ec0-868c-d254e6516b4d"),
                column: "Password",
                value: "$2a$11$IydABnwbF.ogNiqjvb6/8ee7q1PVVLweyz4J4XZ2AMILpZvTEG2kO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CancelBgBtnColor",
                table: "WhiteLabellings");

            migrationBuilder.DropColumn(
                name: "InvoiceTitleBgColor",
                table: "WhiteLabellings");

            migrationBuilder.DropColumn(
                name: "InvoiceTitleColor",
                table: "WhiteLabellings");

            migrationBuilder.DropColumn(
                name: "SaveBtnBgColor",
                table: "WhiteLabellings");

            migrationBuilder.DropColumn(
                name: "TableHeaderBgColor",
                table: "WhiteLabellings");

            migrationBuilder.AddColumn<string>(
                name: "CloseBtnColor",
                table: "WhiteLabellings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5f8d5614-2c7e-4ec0-868c-d254e6516b4d"),
                column: "Password",
                value: "$2a$11$PZbvc/yzWRnJ4AwjKogdNuzsAeJAUY20fRdB19P.UcCf0oJMxfx.m");
        }
    }
}
