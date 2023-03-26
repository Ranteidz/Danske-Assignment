using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Number = table.Column<long>(type: "INTEGER", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    Market = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationDto",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    DateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CompanyDtoId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationDto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationDto_Companies_CompanyDtoId",
                        column: x => x.CompanyDtoId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_Number",
                table: "Companies",
                column: "Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NotificationDto_CompanyDtoId",
                table: "NotificationDto",
                column: "CompanyDtoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationDto");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
