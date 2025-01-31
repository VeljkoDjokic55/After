using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AFTER.DAL.Migrations
{
    public partial class missingContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dts_Slrn_SlrnId",
                table: "Dts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Slrn",
                table: "Slrn");

            migrationBuilder.RenameTable(
                name: "Slrn",
                newName: "Slrns");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Slrns",
                table: "Slrns",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Buildings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SlrnId = table.Column<int>(type: "int", nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UtilityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buildings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Buildings_Slrns_SlrnId",
                        column: x => x.SlrnId,
                        principalTable: "Slrns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Meters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SlrnId = table.Column<int>(type: "int", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: true),
                    Condition = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UtilityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meters_Slrns_SlrnId",
                        column: x => x.SlrnId,
                        principalTable: "Slrns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Poles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SlrnId = table.Column<int>(type: "int", nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DtId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UtilityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Poles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Poles_Dts_DtId",
                        column: x => x.DtId,
                        principalTable: "Dts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Poles_Slrns_SlrnId",
                        column: x => x.SlrnId,
                        principalTable: "Slrns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DtId = table.Column<int>(type: "int", nullable: true),
                    BuildingId = table.Column<int>(type: "int", nullable: true),
                    AreaId = table.Column<int>(type: "int", nullable: true),
                    IsNew = table.Column<bool>(type: "bit", nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tariff = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UtilityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_Areas_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customers_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Buildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customers_Dts_DtId",
                        column: x => x.DtId,
                        principalTable: "Dts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_SlrnId",
                table: "Buildings",
                column: "SlrnId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_AreaId",
                table: "Customers",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_BuildingId",
                table: "Customers",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_DtId",
                table: "Customers",
                column: "DtId");

            migrationBuilder.CreateIndex(
                name: "IX_Meters_SlrnId",
                table: "Meters",
                column: "SlrnId");

            migrationBuilder.CreateIndex(
                name: "IX_Poles_DtId",
                table: "Poles",
                column: "DtId");

            migrationBuilder.CreateIndex(
                name: "IX_Poles_SlrnId",
                table: "Poles",
                column: "SlrnId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dts_Slrns_SlrnId",
                table: "Dts",
                column: "SlrnId",
                principalTable: "Slrns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dts_Slrns_SlrnId",
                table: "Dts");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Meters");

            migrationBuilder.DropTable(
                name: "Poles");

            migrationBuilder.DropTable(
                name: "Buildings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Slrns",
                table: "Slrns");

            migrationBuilder.RenameTable(
                name: "Slrns",
                newName: "Slrn");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Slrn",
                table: "Slrn",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Dts_Slrn_SlrnId",
                table: "Dts",
                column: "SlrnId",
                principalTable: "Slrn",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
