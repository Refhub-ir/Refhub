using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Refhub.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeletedOnBookTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
     
      

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Books",
                type: "bit",
                nullable: false,
                defaultValue: false);

     
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
      

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Books");

          

    
        }
    }
}
