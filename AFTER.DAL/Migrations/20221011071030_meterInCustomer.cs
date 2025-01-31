using Microsoft.EntityFrameworkCore.Migrations;

namespace AFTER.DAL.Migrations
{
    public partial class meterInCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MeterId",
                table: "Customers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_MeterId",
                table: "Customers",
                column: "MeterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Meters_MeterId",
                table: "Customers",
                column: "MeterId",
                principalTable: "Meters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Meters_MeterId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_MeterId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "MeterId",
                table: "Customers");
        }
    }
}
