using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    public partial class FixedTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_Users_UserId",
                table: "Quotes");

            migrationBuilder.DropIndex(
                name: "IX_Quotes_UserId",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Quotes");

            migrationBuilder.AddColumn<string>(
                name: "AboutPhilosopher",
                table: "Quotes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Explanation",
                table: "Quotes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Quotes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Philosopher",
                table: "Quotes",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AboutPhilosopher",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "Explanation",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "Philosopher",
                table: "Quotes");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Quotes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Quotes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_UserId",
                table: "Quotes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_Users_UserId",
                table: "Quotes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
