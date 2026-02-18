using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalFinanceApplication_DAL.Migrations
{
    /// <inheritdoc />
    public partial class addsoftdelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "UserContract",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "ScheduledSalary",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Income",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Expense",
                type: "bit",
                nullable: false,
                defaultValue: false);

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
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Income",
                keyColumn: "IncomeId",
                keyValue: 1,
                columns: new[] { "Date", "IsActive", "ReferenceId" },
                values: new object[] { new DateTime(2026, 2, 18, 0, 0, 0, 0, DateTimeKind.Local), true, new Guid("ea1fb836-69c7-4698-b9d7-d7a26b7be0ec") });

            migrationBuilder.UpdateData(
                table: "ScheduledSalary",
                keyColumn: "SalarySchedulerId",
                keyValue: 1,
                columns: new[] { "Currency", "LastExecutedAt", "ReferenceId" },
                values: new object[] { "MKD", new DateTime(2026, 2, 18, 0, 0, 0, 0, DateTimeKind.Local), new Guid("d6d59133-dfc3-4e6e-a95b-3994f06d29b0") });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 1,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "UserContract",
                keyColumn: "UserContractId",
                keyValue: 1,
                columns: new[] { "DateOpened", "IsActive" },
                values: new object[] { new DateTime(2026, 2, 18, 0, 0, 0, 0, DateTimeKind.Local), true });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "UserContract");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "ScheduledSalary");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Income");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Expense");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AccountBalance");

            migrationBuilder.UpdateData(
                table: "Income",
                keyColumn: "IncomeId",
                keyValue: 1,
                columns: new[] { "Date", "ReferenceId" },
                values: new object[] { new DateTime(2026, 2, 16, 0, 0, 0, 0, DateTimeKind.Local), new Guid("c5961183-093d-4d93-b2ff-c0d70b936967") });

            migrationBuilder.UpdateData(
                table: "ScheduledSalary",
                keyColumn: "SalarySchedulerId",
                keyValue: 1,
                columns: new[] { "LastExecutedAt", "ReferenceId" },
                values: new object[] { new DateTime(2026, 2, 16, 0, 0, 0, 0, DateTimeKind.Local), new Guid("de945c4a-16be-460a-b842-c8602a6aede0") });

            migrationBuilder.UpdateData(
                table: "UserContract",
                keyColumn: "UserContractId",
                keyValue: 1,
                column: "DateOpened",
                value: new DateTime(2026, 2, 16, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
