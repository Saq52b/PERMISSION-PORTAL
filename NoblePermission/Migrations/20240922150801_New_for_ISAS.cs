using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NoblePermission.Migrations
{
    public partial class New_for_ISAS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5f8d5614-2c7e-4ec0-868c-d254e6516b4d"),
                column: "Password",
                value: "$2a$11$DiLKJjXpGAo8rt.EOwGo8O/83Z9WSn3qsa/hxQ66PuBNDOwMATLCe");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5f8d5614-2c7e-4ec0-868c-d254e6516b4d"),
                column: "Password",
                value: "$2a$11$RxtykGPRQKcDBiTKjrFvl.oRJihxr7GRDhZCu4ubhD/ZWisC.eygq");
        }
    }
}
