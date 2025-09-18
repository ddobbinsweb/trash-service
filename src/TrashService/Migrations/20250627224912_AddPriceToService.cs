using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrashService.Migrations
{
    /// <inheritdoc />
    public partial class AddPriceToService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "Services",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Services",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Services");
        }
    }
}
