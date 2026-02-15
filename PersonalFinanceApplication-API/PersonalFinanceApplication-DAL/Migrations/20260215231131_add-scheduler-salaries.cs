using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalFinanceApplication_DAL.Migrations
{
    /// <inheritdoc />
    public partial class addschedulersalaries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ScheduledSalary",
                columns: table => new
                {
                    SalarySchedulerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReferenceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserContractId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DayOfMonth = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LastExecutedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledSalary", x => x.SalarySchedulerId);
                });

            migrationBuilder.UpdateData(
                table: "Income",
                keyColumn: "IncomeId",
                keyValue: 1,
                columns: new[] { "Date", "ReferenceId" },
                values: new object[] { new DateTime(2026, 2, 16, 0, 0, 0, 0, DateTimeKind.Local), new Guid("91545bee-2720-4960-b51b-333178d8ce33") });

            migrationBuilder.UpdateData(
                table: "UserContract",
                keyColumn: "UserContractId",
                keyValue: 1,
                column: "DateOpened",
                value: new DateTime(2026, 2, 16, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledSalary_ReferenceId",
                table: "ScheduledSalary",
                column: "ReferenceId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduledSalary");

            migrationBuilder.UpdateData(
                table: "Income",
                keyColumn: "IncomeId",
                keyValue: 1,
                columns: new[] { "Date", "ReferenceId" },
                values: new object[] { new DateTime(2026, 2, 15, 0, 0, 0, 0, DateTimeKind.Local), new Guid("a7eeffde-f603-4181-a14c-1fb15b16ff5f") });

            migrationBuilder.UpdateData(
                table: "UserContract",
                keyColumn: "UserContractId",
                keyValue: 1,
                column: "DateOpened",
                value: new DateTime(2026, 2, 15, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
