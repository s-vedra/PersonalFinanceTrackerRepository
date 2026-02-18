using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalFinanceApplication_DAL.Migrations
{
    /// <inheritdoc />
    public partial class addcreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "UserContract",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "ScheduledSalary",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Income",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Expense",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "AccountBalance",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AccountBalance",
                keyColumn: "AccountBalanceId",
                keyValue: 1,
                column: "Created",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Income",
                keyColumn: "IncomeId",
                keyValue: 1,
                columns: new[] { "Created", "ReferenceId" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("99317879-581b-4a4a-870e-a8df648a9685") });

            migrationBuilder.UpdateData(
                table: "ScheduledSalary",
                keyColumn: "SalarySchedulerId",
                keyValue: 1,
                columns: new[] { "Created", "ReferenceId" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0759ad85-2127-423e-ba4a-1afddb6c5769") });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 1,
                column: "Created",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "UserContract",
                keyColumn: "UserContractId",
                keyValue: 1,
                column: "Created",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "UserContract");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "ScheduledSalary");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Income");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Expense");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "AccountBalance");

            migrationBuilder.UpdateData(
                table: "Income",
                keyColumn: "IncomeId",
                keyValue: 1,
                column: "ReferenceId",
                value: new Guid("5f1f8875-a836-4a24-b43d-559eccb1ee8f"));

            migrationBuilder.UpdateData(
                table: "ScheduledSalary",
                keyColumn: "SalarySchedulerId",
                keyValue: 1,
                column: "ReferenceId",
                value: new Guid("fc2c884f-2e40-4851-af57-4e11657dc220"));
        }
    }
}
