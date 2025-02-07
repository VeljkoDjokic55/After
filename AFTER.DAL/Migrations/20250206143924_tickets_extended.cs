using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AFTER.DAL.Migrations
{
    /// <inheritdoc />
    public partial class tickets_extended : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ScannedCount",
                table: "Tickets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ScannedCountMax",
                table: "Tickets",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScannedCount",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "ScannedCountMax",
                table: "Tickets");
        }
    }
}
