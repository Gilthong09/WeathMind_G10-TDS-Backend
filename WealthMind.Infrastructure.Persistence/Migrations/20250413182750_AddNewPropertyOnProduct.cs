using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WealthMind.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddNewPropertyOnProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Recommendations");

            migrationBuilder.DropColumn(
                name: "StatisticsId",
                table: "Recommendations");

            migrationBuilder.AddColumn<string>(
                name: "ReportId",
                table: "Recommendations",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Recommendations_ReportId",
                table: "Recommendations",
                column: "ReportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recommendations_Reports_ReportId",
                table: "Recommendations",
                column: "ReportId",
                principalTable: "Reports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recommendations_Reports_ReportId",
                table: "Recommendations");

            migrationBuilder.DropIndex(
                name: "IX_Recommendations_ReportId",
                table: "Recommendations");

            migrationBuilder.DropColumn(
                name: "ReportId",
                table: "Recommendations");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "ProductId",
                table: "Reports",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductId",
                table: "Recommendations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StatisticsId",
                table: "Recommendations",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
