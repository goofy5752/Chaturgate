using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chaturgate.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddThumbnailToLiveStream : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Thumbnail",
                table: "Streams",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Thumbnail",
                table: "Streams");
        }
    }
}
