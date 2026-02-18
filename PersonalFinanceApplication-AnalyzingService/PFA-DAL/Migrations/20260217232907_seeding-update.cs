using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PFA_DAL.Migrations
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
        }
    }
}
