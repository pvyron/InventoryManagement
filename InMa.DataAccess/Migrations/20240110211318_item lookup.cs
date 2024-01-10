using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InMa.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class itemlookup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Items_Name",
                table: "Items");

            migrationBuilder.AddColumn<string>(
                name: "CategoryNameLookup",
                table: "Items",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameLookup",
                table: "Items",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Items_NameLookup",
                table: "Items",
                column: "NameLookup",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Items_NameLookup",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "CategoryNameLookup",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "NameLookup",
                table: "Items");

            migrationBuilder.CreateIndex(
                name: "IX_Items_Name",
                table: "Items",
                column: "Name",
                unique: true);
        }
    }
}
