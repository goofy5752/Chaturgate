using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chaturgate.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeStreamStatusFromEnumToDbEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Streams");

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Streams",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StreamStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StreamStatus", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Streams_StatusId",
                table: "Streams",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Streams_StreamStatus_StatusId",
                table: "Streams",
                column: "StatusId",
                principalTable: "StreamStatus",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Streams_StreamStatus_StatusId",
                table: "Streams");

            migrationBuilder.DropTable(
                name: "StreamStatus");

            migrationBuilder.DropIndex(
                name: "IX_Streams_StatusId",
                table: "Streams");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Streams");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Streams",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
