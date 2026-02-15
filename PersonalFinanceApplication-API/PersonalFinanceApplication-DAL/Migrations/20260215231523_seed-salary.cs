using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalFinanceApplication_DAL.Migrations
{
    /// <inheritdoc />
    public partial class seedsalary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "ScheduledSalary",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Income",
                keyColumn: "IncomeId",
                keyValue: 1,
                column: "ReferenceId",
                value: new Guid("c5961183-093d-4d93-b2ff-c0d70b936967"));

            migrationBuilder.InsertData(
                table: "ScheduledSalary",
                columns: new[] { "SalarySchedulerId", "Amount", "DayOfMonth", "IsActive", "LastExecutedAt", "Notes", "ReferenceId", "UserContractId" },
                values: new object[] { 1, 57890m, 1, true, new DateTime(2026, 2, 16, 0, 0, 0, 0, DateTimeKind.Local), null, new Guid("de945c4a-16be-460a-b842-c8602a6aede0"), 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ScheduledSalary",
                keyColumn: "SalarySchedulerId",
                keyValue: 1);

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "ScheduledSalary",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Income",
                keyColumn: "IncomeId",
                keyValue: 1,
                column: "ReferenceId",
                value: new Guid("91545bee-2720-4960-b51b-333178d8ce33"));
        }
    }
}
