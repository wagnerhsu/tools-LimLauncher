using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreSqlite.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GroupInfo",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    GroupName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupInfo", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ShortcutInfo",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    GroupID = table.Column<Guid>(type: "TEXT", nullable: false),
                    FileFullPath = table.Column<string>(type: "TEXT", nullable: false),
                    FileName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShortcutInfo", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupInfo");

            migrationBuilder.DropTable(
                name: "ShortcutInfo");
        }
    }
}
