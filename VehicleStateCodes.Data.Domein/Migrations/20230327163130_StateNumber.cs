using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleStateCodes.Data.Domein.Migrations
{
    /// <inheritdoc />
    public partial class StateNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StateNumber",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    CreateTime = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateNumber", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StateNumberOrder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateNumberId = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateNumberOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StateNumberOrder_StateNumber_StateNumberId",
                        column: x => x.StateNumberId,
                        principalTable: "StateNumber",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StateNumberReservation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateNumberId = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "date", nullable: false),
                    EndReservation = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateNumberReservation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StateNumberReservation_StateNumber_StateNumberId",
                        column: x => x.StateNumberId,
                        principalTable: "StateNumber",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StateNumberOrder_StateNumberId",
                table: "StateNumberOrder",
                column: "StateNumberId");

            migrationBuilder.CreateIndex(
                name: "IX_StateNumberReservation_StateNumberId",
                table: "StateNumberReservation",
                column: "StateNumberId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StateNumberOrder");

            migrationBuilder.DropTable(
                name: "StateNumberReservation");

            migrationBuilder.DropTable(
                name: "StateNumber");
        }
    }
}
