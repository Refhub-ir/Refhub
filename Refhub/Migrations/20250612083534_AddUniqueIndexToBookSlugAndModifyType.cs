using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Refhub.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueIndexToBookSlugAndModifyType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "Books",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.Sql(@"
                IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Books_Slug' AND object_id = OBJECT_ID('Books'))
                BEGIN
                    DROP INDEX IX_Books_Slug ON Books;
                END");
            migrationBuilder.CreateIndex(
                name: "IX_Books_Slug",
                table: "Books",
                column: "Slug",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Books_Slug",
                table: "Books");

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
