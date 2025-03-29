using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WealthMind.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixingErrors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryTypes");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "FinancialGoals");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "FinancialGoals",
                newName: "Name");

            migrationBuilder.AlterColumn<string>(
                name: "StatisticsId",
                table: "Reports",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "ProductId",
                table: "Reports",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StatisticsId",
                table: "Recommendations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "ProductId",
                table: "Recommendations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductId",
                table: "FinancialGoals",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SavingId",
                table: "FinancialGoals",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FinancialGoals_ProductId",
                table: "FinancialGoals",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialGoals_SavingId",
                table: "FinancialGoals",
                column: "SavingId");

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialGoals_Products_ProductId",
                table: "FinancialGoals",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialGoals_Products_SavingId",
                table: "FinancialGoals",
                column: "SavingId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinancialGoals_Products_ProductId",
                table: "FinancialGoals");

            migrationBuilder.DropForeignKey(
                name: "FK_FinancialGoals_Products_SavingId",
                table: "FinancialGoals");

            migrationBuilder.DropIndex(
                name: "IX_FinancialGoals_ProductId",
                table: "FinancialGoals");

            migrationBuilder.DropIndex(
                name: "IX_FinancialGoals_SavingId",
                table: "FinancialGoals");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Recommendations");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "FinancialGoals");

            migrationBuilder.DropColumn(
                name: "SavingId",
                table: "FinancialGoals");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "FinancialGoals",
                newName: "Type");

            migrationBuilder.AlterColumn<string>(
                name: "StatisticsId",
                table: "Reports",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StatisticsId",
                table: "Recommendations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "FinancialGoals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "CategoryTypes",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryTypes", x => x.Name);
                });
        }
    }
}
