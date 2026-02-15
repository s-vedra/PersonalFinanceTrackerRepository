using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalFinanceApplication_DAL.Migrations
{
    /// <inheritdoc />
    public partial class addguidconstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ReferenceId",
                table: "Income",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ReferenceId",
                table: "Expense",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "AccountBalance",
                keyColumn: "AccountBalanceId",
                keyValue: 1,
                column: "Amount",
                value: 1070000m);

            migrationBuilder.UpdateData(
                table: "Income",
                keyColumn: "IncomeId",
                keyValue: 1,
                column: "ReferenceId",
                value: new Guid("a7eeffde-f603-4181-a14c-1fb15b16ff5f"));

            migrationBuilder.CreateIndex(
                name: "IX_Income_ReferenceId",
                table: "Income",
                column: "ReferenceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Expense_ReferenceId",
                table: "Expense",
                column: "ReferenceId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Income_ReferenceId",
                table: "Income");

            migrationBuilder.DropIndex(
                name: "IX_Expense_ReferenceId",
                table: "Expense");

            migrationBuilder.DropColumn(
                name: "ReferenceId",
                table: "Income");

            migrationBuilder.DropColumn(
                name: "ReferenceId",
                table: "Expense");

            migrationBuilder.UpdateData(
                table: "AccountBalance",
                keyColumn: "AccountBalanceId",
                keyValue: 1,
                column: "Amount",
                value: 107000000m);
        }
    }
}
