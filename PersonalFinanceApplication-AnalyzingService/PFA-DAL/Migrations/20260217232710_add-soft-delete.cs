using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PFA_DAL.Migrations
{
    /// <inheritdoc />
    public partial class addsoftdelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AccountBalance",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AccountBalance",
                keyColumn: "AccountBalanceId",
                keyValue: 1,
                columns: new[] { "IsActive", "LastDateAddedMoney", "LastDateDrawMoney" },
                values: new object[] { false, new DateTime(2026, 2, 18, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 2, 16, 0, 0, 0, 0, DateTimeKind.Local) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AccountBalance");

            migrationBuilder.UpdateData(
                table: "AccountBalance",
                keyColumn: "AccountBalanceId",
                keyValue: 1,
                columns: new[] { "LastDateAddedMoney", "LastDateDrawMoney" },
                values: new object[] { new DateTime(2024, 11, 22, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 11, 20, 0, 0, 0, 0, DateTimeKind.Local) });
        }
    }
}
