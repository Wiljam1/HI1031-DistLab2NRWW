using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionApplication.Migrations
{
    /// <inheritdoc />
    public partial class BidDbs_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AuctionDbs",
                keyColumn: "Id",
                keyValue: -1,
                column: "CreatedDate",
                value: new DateTime(2023, 10, 19, 16, 7, 11, 399, DateTimeKind.Local).AddTicks(9145));

            migrationBuilder.UpdateData(
                table: "BidDbs",
                keyColumn: "Id",
                keyValue: -2,
                column: "LastUpdated",
                value: new DateTime(2023, 10, 19, 16, 7, 11, 399, DateTimeKind.Local).AddTicks(9315));

            migrationBuilder.UpdateData(
                table: "BidDbs",
                keyColumn: "Id",
                keyValue: -1,
                column: "LastUpdated",
                value: new DateTime(2023, 10, 19, 16, 7, 11, 399, DateTimeKind.Local).AddTicks(9312));
        }
    }
}
