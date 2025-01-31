using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AFTER.DAL.Migrations
{
    public partial class slrn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slrn",
                table: "Dts");

            migrationBuilder.AddColumn<int>(
                name: "SlrnId",
                table: "Dts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Slrn",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntityType = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slrn", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dts_SlrnId",
                table: "Dts",
                column: "SlrnId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dts_Slrn_SlrnId",
                table: "Dts",
                column: "SlrnId",
                principalTable: "Slrn",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dts_Slrn_SlrnId",
                table: "Dts");

            migrationBuilder.DropTable(
                name: "Slrn");

            migrationBuilder.DropIndex(
                name: "IX_Dts_SlrnId",
                table: "Dts");

            migrationBuilder.DropColumn(
                name: "SlrnId",
                table: "Dts");

            migrationBuilder.AddColumn<string>(
                name: "Slrn",
                table: "Dts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
