using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PFA_DAL.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountBalance",
                columns: table => new
                {
                    AccountBalanceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastDateAddedMoney = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastDateDrawMoney = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountBalance", x => x.AccountBalanceId);
                });

            migrationBuilder.InsertData(
                table: "AccountBalance",
                columns: new[] { "AccountBalanceId", "Amount", "Currency", "LastDateAddedMoney", "LastDateDrawMoney" },
                values: new object[] { 1, 10000m, "MKD", new DateTime(2024, 11, 21, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 11, 19, 0, 0, 0, 0, DateTimeKind.Local) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountBalance");
        }
    }
}
