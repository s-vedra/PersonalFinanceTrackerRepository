using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalFinanceApplication_DAL.Migrations
{
    /// <inheritdoc />
    public partial class addcontractstatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "UserContract",
                keyColumn: "UserContractId",
                keyValue: 1,
                column: "UserContractStatus",
                value: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "UserContract",
                keyColumn: "UserContractId",
                keyValue: 1,
                column: "UserContractStatus",
                value: 0);
        }
    }
}
