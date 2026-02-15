using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalFinanceApplication_DAL.Migrations
{
    /// <inheritdoc />
    public partial class addbalanceoncontract : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Income",
                type: "decimal(18,6)",
                precision: 18,
                scale: 6,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Expense",
                type: "decimal(18,6)",
                precision: 18,
                scale: 6,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.CreateTable(
                name: "AccountBalance",
                columns: table => new
                {
                    AccountBalanceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastDateAddedMoney = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastDateDrawMoney = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserContractId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountBalance", x => x.AccountBalanceId);
                });

            migrationBuilder.InsertData(
                table: "AccountBalance",
                columns: new[] { "AccountBalanceId", "Amount", "Currency", "LastDateAddedMoney", "LastDateDrawMoney", "UserContractId" },
                values: new object[] { 1, 107000000m, "MKD", new DateTime(2025, 12, 22, 20, 14, 18, 586, DateTimeKind.Unspecified), new DateTime(2025, 12, 22, 20, 14, 18, 586, DateTimeKind.Unspecified), 1 });

            migrationBuilder.UpdateData(
                table: "Income",
                keyColumn: "IncomeId",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2026, 2, 15, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "UserContract",
                keyColumn: "UserContractId",
                keyValue: 1,
                column: "DateOpened",
                value: new DateTime(2026, 2, 15, 0, 0, 0, 0, DateTimeKind.Local));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountBalance");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Income",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)",
                oldPrecision: 18,
                oldScale: 6);

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Expense",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)",
                oldPrecision: 18,
                oldScale: 6);

            migrationBuilder.UpdateData(
                table: "Income",
                keyColumn: "IncomeId",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 12, 3, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "UserContract",
                keyColumn: "UserContractId",
                keyValue: 1,
                column: "DateOpened",
                value: new DateTime(2024, 12, 3, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
