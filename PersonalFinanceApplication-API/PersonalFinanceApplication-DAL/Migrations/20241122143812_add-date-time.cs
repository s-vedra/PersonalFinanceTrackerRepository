using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalFinanceApplication_DAL.Migrations
{
    /// <inheritdoc />
    public partial class adddatetime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Account",
                table: "Income",
                newName: "PaymentIssue");

            migrationBuilder.RenameColumn(
                name: "Account",
                table: "Expenses",
                newName: "PaymentIssue");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOpened",
                table: "UserContract",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "UserContract",
                keyColumn: "UserContractId",
                keyValue: 1,
                column: "DateOpened",
                value: new DateTime(2024, 11, 22, 0, 0, 0, 0, DateTimeKind.Local));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOpened",
                table: "UserContract");

            migrationBuilder.RenameColumn(
                name: "PaymentIssue",
                table: "Income",
                newName: "Account");

            migrationBuilder.RenameColumn(
                name: "PaymentIssue",
                table: "Expenses",
                newName: "Account");
        }
    }
}
