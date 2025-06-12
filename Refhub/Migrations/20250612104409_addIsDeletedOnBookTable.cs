using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Refhub.Migrations
{
    /// <inheritdoc />
    public partial class addIsDeletedOnBookTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Books",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Books");
        }
    }
}
