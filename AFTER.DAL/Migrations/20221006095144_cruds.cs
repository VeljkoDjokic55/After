using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AFTER.DAL.Migrations
{
    public partial class cruds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Areas_Regions_RegionId",
                table: "Areas");

            migrationBuilder.AlterColumn<int>(
                name: "RegionId",
                table: "Areas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "TransmissionStations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransmissionStations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Feeder33s",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransmissionStationId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feeder33s", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feeder33s_TransmissionStations_TransmissionStationId",
                        column: x => x.TransmissionStationId,
                        principalTable: "TransmissionStations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Substations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeederId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Substations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Substations_Feeder33s_FeederId",
                        column: x => x.FeederId,
                        principalTable: "Feeder33s",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Feeder11s",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubstationId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feeder11s", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feeder11s_Substations_SubstationId",
                        column: x => x.SubstationId,
                        principalTable: "Substations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Feeder33Sss",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeederId = table.Column<int>(type: "int", nullable: true),
                    SubstationId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feeder33Sss", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feeder33Sss_Feeder33s_FeederId",
                        column: x => x.FeederId,
                        principalTable: "Feeder33s",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Feeder33Sss_Substations_SubstationId",
                        column: x => x.SubstationId,
                        principalTable: "Substations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Dts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Slrn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AreaId = table.Column<int>(type: "int", nullable: true),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Feeder11Id = table.Column<int>(type: "int", nullable: true),
                    Feeder33Id = table.Column<int>(type: "int", nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dts_Areas_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Dts_Feeder11s_Feeder11Id",
                        column: x => x.Feeder11Id,
                        principalTable: "Feeder11s",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Dts_Feeder33s_Feeder33Id",
                        column: x => x.Feeder33Id,
                        principalTable: "Feeder33s",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dts_AreaId",
                table: "Dts",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Dts_Feeder11Id",
                table: "Dts",
                column: "Feeder11Id");

            migrationBuilder.CreateIndex(
                name: "IX_Dts_Feeder33Id",
                table: "Dts",
                column: "Feeder33Id");

            migrationBuilder.CreateIndex(
                name: "IX_Feeder11s_SubstationId",
                table: "Feeder11s",
                column: "SubstationId");

            migrationBuilder.CreateIndex(
                name: "IX_Feeder33s_TransmissionStationId",
                table: "Feeder33s",
                column: "TransmissionStationId");

            migrationBuilder.CreateIndex(
                name: "IX_Feeder33Sss_FeederId",
                table: "Feeder33Sss",
                column: "FeederId");

            migrationBuilder.CreateIndex(
                name: "IX_Feeder33Sss_SubstationId",
                table: "Feeder33Sss",
                column: "SubstationId");

            migrationBuilder.CreateIndex(
                name: "IX_Substations_FeederId",
                table: "Substations",
                column: "FeederId");

            migrationBuilder.AddForeignKey(
                name: "FK_Areas_Regions_RegionId",
                table: "Areas",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Areas_Regions_RegionId",
                table: "Areas");

            migrationBuilder.DropTable(
                name: "Dts");

            migrationBuilder.DropTable(
                name: "Feeder33Sss");

            migrationBuilder.DropTable(
                name: "Feeder11s");

            migrationBuilder.DropTable(
                name: "Substations");

            migrationBuilder.DropTable(
                name: "Feeder33s");

            migrationBuilder.DropTable(
                name: "TransmissionStations");

            migrationBuilder.AlterColumn<int>(
                name: "RegionId",
                table: "Areas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Areas_Regions_RegionId",
                table: "Areas",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
