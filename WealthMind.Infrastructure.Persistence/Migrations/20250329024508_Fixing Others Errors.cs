using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WealthMind.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixingOthersErrors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CreditCard_Debt",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreditCard_Debt",
                table: "Products");
        }
    }
}
