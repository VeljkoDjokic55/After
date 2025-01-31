using Microsoft.EntityFrameworkCore.Migrations;

namespace AFTER.DAL.Migrations
{
    public partial class customer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UtilityId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UtilityId",
                table: "TransmissionStations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UtilityId",
                table: "Substations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UtilityId",
                table: "Slrn",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UtilityId",
                table: "Regions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UtilityId",
                table: "Feeder33Sss",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UtilityId",
                table: "Feeder33s",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UtilityId",
                table: "Feeder11s",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UtilityId",
                table: "Dts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UtilityId",
                table: "AuditLogs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UtilityId",
                table: "Areas",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UtilityId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UtilityId",
                table: "TransmissionStations");

            migrationBuilder.DropColumn(
                name: "UtilityId",
                table: "Substations");

            migrationBuilder.DropColumn(
                name: "UtilityId",
                table: "Slrn");

            migrationBuilder.DropColumn(
                name: "UtilityId",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "UtilityId",
                table: "Feeder33Sss");

            migrationBuilder.DropColumn(
                name: "UtilityId",
                table: "Feeder33s");

            migrationBuilder.DropColumn(
                name: "UtilityId",
                table: "Feeder11s");

            migrationBuilder.DropColumn(
                name: "UtilityId",
                table: "Dts");

            migrationBuilder.DropColumn(
                name: "UtilityId",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "UtilityId",
                table: "Areas");
        }
    }
}
