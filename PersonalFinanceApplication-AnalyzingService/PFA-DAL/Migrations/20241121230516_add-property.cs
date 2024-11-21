using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PFA_DAL.Migrations
{
    /// <inheritdoc />
    public partial class addproperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserContractId",
                table: "AccountBalance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AccountBalance",
                keyColumn: "AccountBalanceId",
                keyValue: 1,
                columns: new[] { "LastDateAddedMoney", "LastDateDrawMoney", "UserContractId" },
                values: new object[] { new DateTime(2024, 11, 22, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 11, 20, 0, 0, 0, 0, DateTimeKind.Local), 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserContractId",
                table: "AccountBalance");

            migrationBuilder.UpdateData(
                table: "AccountBalance",
                keyColumn: "AccountBalanceId",
                keyValue: 1,
                columns: new[] { "LastDateAddedMoney", "LastDateDrawMoney" },
                values: new object[] { new DateTime(2024, 11, 21, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 11, 19, 0, 0, 0, 0, DateTimeKind.Local) });
        }
    }
}
