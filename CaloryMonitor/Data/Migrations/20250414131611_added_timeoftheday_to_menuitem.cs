using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaloryMonitor.Data.Migrations
{
    /// <inheritdoc />
    public partial class added_timeoftheday_to_menuitem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TimeOfTheDay",
                table: "MenuItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeOfTheDay",
                table: "MenuItems");
        }
    }
}
