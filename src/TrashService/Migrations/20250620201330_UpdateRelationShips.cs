using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrashService.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelationShips : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Services_Id",
                table: "Services",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_Id",
                table: "Payments",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Id",
                table: "Customers",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Services_Id",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Payments_Id",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Customers_Id",
                table: "Customers");
        }
    }
}
