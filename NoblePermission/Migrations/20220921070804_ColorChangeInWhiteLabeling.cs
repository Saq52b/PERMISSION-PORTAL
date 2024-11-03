using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NoblePermission.Migrations
{
    public partial class ColorChangeInWhiteLabeling : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CancelBtnColor",
                table: "WhiteLabellings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CloseBtnColor",
                table: "WhiteLabellings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeadingColor",
                table: "WhiteLabellings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SaveBtnColor",
                table: "WhiteLabellings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SideMenuBtnClickColor",
                table: "WhiteLabellings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SideMenuBtnColor",
                table: "WhiteLabellings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SideMenuColor",
                table: "WhiteLabellings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TableHeaderColor",
                table: "WhiteLabellings",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5f8d5614-2c7e-4ec0-868c-d254e6516b4d"),
                column: "Password",
                value: "$2a$11$PZbvc/yzWRnJ4AwjKogdNuzsAeJAUY20fRdB19P.UcCf0oJMxfx.m");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CancelBtnColor",
                table: "WhiteLabellings");

            migrationBuilder.DropColumn(
                name: "CloseBtnColor",
                table: "WhiteLabellings");

            migrationBuilder.DropColumn(
                name: "HeadingColor",
                table: "WhiteLabellings");

            migrationBuilder.DropColumn(
                name: "SaveBtnColor",
                table: "WhiteLabellings");

            migrationBuilder.DropColumn(
                name: "SideMenuBtnClickColor",
                table: "WhiteLabellings");

            migrationBuilder.DropColumn(
                name: "SideMenuBtnColor",
                table: "WhiteLabellings");

            migrationBuilder.DropColumn(
                name: "SideMenuColor",
                table: "WhiteLabellings");

            migrationBuilder.DropColumn(
                name: "TableHeaderColor",
                table: "WhiteLabellings");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5f8d5614-2c7e-4ec0-868c-d254e6516b4d"),
                column: "Password",
                value: "$2a$11$SM4ZxMmwdwiYZ6eAylcMOeVk02X0R/ZSf/M1hOL8FoU93f8wumU82");
        }
    }
}
