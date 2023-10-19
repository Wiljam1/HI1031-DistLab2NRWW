using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionApplication.Migrations
{
    /// <inheritdoc />
    public partial class AuctionDbb_Username : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "AuctionDbs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "AuctionDetailsVM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionDetailsVM", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CreateAuctionVM",
                columns: table => new
                {
                    Title = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "BidVM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    AuctionDetailsVMId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BidVM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BidVM_AuctionDetailsVM_AuctionDetailsVMId",
                        column: x => x.AuctionDetailsVMId,
                        principalTable: "AuctionDetailsVM",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AuctionDbs",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "CreatedDate", "UserName" },
                values: new object[] { new DateTime(2023, 10, 19, 19, 8, 49, 87, DateTimeKind.Local).AddTicks(2935), "wiljam@kth.se" });

            migrationBuilder.UpdateData(
                table: "BidDbs",
                keyColumn: "Id",
                keyValue: -2,
                column: "LastUpdated",
                value: new DateTime(2023, 10, 19, 19, 8, 49, 87, DateTimeKind.Local).AddTicks(3141));

            migrationBuilder.UpdateData(
                table: "BidDbs",
                keyColumn: "Id",
                keyValue: -1,
                column: "LastUpdated",
                value: new DateTime(2023, 10, 19, 19, 8, 49, 87, DateTimeKind.Local).AddTicks(3138));

            migrationBuilder.CreateIndex(
                name: "IX_BidVM_AuctionDetailsVMId",
                table: "BidVM",
                column: "AuctionDetailsVMId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BidVM");

            migrationBuilder.DropTable(
                name: "CreateAuctionVM");

            migrationBuilder.DropTable(
                name: "AuctionDetailsVM");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "AuctionDbs");

            migrationBuilder.UpdateData(
                table: "AuctionDbs",
                keyColumn: "Id",
                keyValue: -1,
                column: "CreatedDate",
                value: new DateTime(2023, 10, 19, 16, 58, 55, 742, DateTimeKind.Local).AddTicks(2298));

            migrationBuilder.UpdateData(
                table: "BidDbs",
                keyColumn: "Id",
                keyValue: -2,
                column: "LastUpdated",
                value: new DateTime(2023, 10, 19, 16, 58, 55, 742, DateTimeKind.Local).AddTicks(2487));

            migrationBuilder.UpdateData(
                table: "BidDbs",
                keyColumn: "Id",
                keyValue: -1,
                column: "LastUpdated",
                value: new DateTime(2023, 10, 19, 16, 58, 55, 742, DateTimeKind.Local).AddTicks(2484));
        }
    }
}
