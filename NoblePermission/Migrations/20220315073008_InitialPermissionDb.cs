using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NoblePermission.Migrations
{
    public partial class InitialPermissionDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NobleGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    GroupName = table.Column<string>(nullable: true),
                    GroupType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NobleGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NobleModules",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NobleModules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NoblePermissions",
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
                    table.PrimaryKey("PK_NoblePermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NoblePermissions_NobleGroups_NobleGroupId",
                        column: x => x.NobleGroupId,
                        principalTable: "NobleGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NoblePermissions_NobleModules_NobleModuleId",
                        column: x => x.NobleModuleId,
                        principalTable: "NobleModules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NoblePermissions_NobleGroupId",
                table: "NoblePermissions",
                column: "NobleGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_NoblePermissions_NobleModuleId",
                table: "NoblePermissions",
                column: "NobleModuleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NoblePermissions");

            migrationBuilder.DropTable(
                name: "NobleGroups");

            migrationBuilder.DropTable(
                name: "NobleModules");
        }
    }
}
