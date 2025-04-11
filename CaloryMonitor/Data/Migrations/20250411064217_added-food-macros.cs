using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaloryMonitor.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedfoodmacros : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "CarbsPer100g",
                table: "Foods",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "FatsPer100g",
                table: "Foods",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ProteinsPer100g",
                table: "Foods",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarbsPer100g",
                table: "Foods");

            migrationBuilder.DropColumn(
                name: "FatsPer100g",
                table: "Foods");

            migrationBuilder.DropColumn(
                name: "ProteinsPer100g",
                table: "Foods");
        }
    }
}
