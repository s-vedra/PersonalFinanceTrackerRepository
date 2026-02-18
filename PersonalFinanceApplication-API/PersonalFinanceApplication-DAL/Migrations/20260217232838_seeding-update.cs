using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalFinanceApplication_DAL.Migrations
{
    /// <inheritdoc />
    public partial class seedingupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AccountBalance",
                keyColumn: "AccountBalanceId",
                keyValue: 1,
                column: "IsActive",
                value: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                column: "ReferenceId",
                value: new Guid("ea1fb836-69c7-4698-b9d7-d7a26b7be0ec"));

            migrationBuilder.UpdateData(
                table: "ScheduledSalary",
                keyColumn: "SalarySchedulerId",
                keyValue: 1,
                column: "ReferenceId",
                value: new Guid("d6d59133-dfc3-4e6e-a95b-3994f06d29b0"));
        }
    }
}
