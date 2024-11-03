using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NoblePermission.Migrations
{
    public partial class applicationColorInWhiteLabeleing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationBgColor",
                table: "WhiteLabellings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationTextColor",
                table: "WhiteLabellings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CardBgColor",
                table: "WhiteLabellings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CardTextColor",
                table: "WhiteLabellings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SetupBgColor",
                table: "WhiteLabellings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SetupCssFilter",
                table: "WhiteLabellings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SetupTextColor",
                table: "WhiteLabellings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SideMenuBtnClickBgColor",
                table: "WhiteLabellings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SideMenuBtnClickColorFilter",
                table: "WhiteLabellings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SideMenuBtnColorFilter",
                table: "WhiteLabellings",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5f8d5614-2c7e-4ec0-868c-d254e6516b4d"),
                column: "Password",
                value: "$2a$11$RxtykGPRQKcDBiTKjrFvl.oRJihxr7GRDhZCu4ubhD/ZWisC.eygq");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationBgColor",
                table: "WhiteLabellings");

            migrationBuilder.DropColumn(
                name: "ApplicationTextColor",
                table: "WhiteLabellings");

            migrationBuilder.DropColumn(
                name: "CardBgColor",
                table: "WhiteLabellings");

            migrationBuilder.DropColumn(
                name: "CardTextColor",
                table: "WhiteLabellings");

            migrationBuilder.DropColumn(
                name: "SetupBgColor",
                table: "WhiteLabellings");

            migrationBuilder.DropColumn(
                name: "SetupCssFilter",
                table: "WhiteLabellings");

            migrationBuilder.DropColumn(
                name: "SetupTextColor",
                table: "WhiteLabellings");

            migrationBuilder.DropColumn(
                name: "SideMenuBtnClickBgColor",
                table: "WhiteLabellings");

            migrationBuilder.DropColumn(
                name: "SideMenuBtnClickColorFilter",
                table: "WhiteLabellings");

            migrationBuilder.DropColumn(
                name: "SideMenuBtnColorFilter",
                table: "WhiteLabellings");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5f8d5614-2c7e-4ec0-868c-d254e6516b4d"),
                column: "Password",
                value: "$2a$11$IydABnwbF.ogNiqjvb6/8ee7q1PVVLweyz4J4XZ2AMILpZvTEG2kO");
        }
    }
}
