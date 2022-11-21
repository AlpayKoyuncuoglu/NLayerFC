using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NLayer.Repository.Migrations
{
    public partial class baseDto_Changed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 21, 9, 26, 0, 637, DateTimeKind.Local).AddTicks(1908));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 21, 9, 26, 0, 637, DateTimeKind.Local).AddTicks(1923));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 21, 9, 26, 0, 637, DateTimeKind.Local).AddTicks(1924));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 21, 9, 26, 0, 637, DateTimeKind.Local).AddTicks(1925));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 21, 9, 26, 0, 637, DateTimeKind.Local).AddTicks(1927));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 17, 16, 28, 36, 593, DateTimeKind.Local).AddTicks(5120));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 17, 16, 28, 36, 593, DateTimeKind.Local).AddTicks(5133));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 17, 16, 28, 36, 593, DateTimeKind.Local).AddTicks(5135));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 17, 16, 28, 36, 593, DateTimeKind.Local).AddTicks(5136));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 17, 16, 28, 36, 593, DateTimeKind.Local).AddTicks(5137));
        }
    }
}
