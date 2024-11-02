using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace efcoreApp.Migrations
{
    /// <inheritdoc />
    public partial class Ogrenci : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OgrenciId",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_OgrenciId",
                table: "AspNetUsers",
                column: "OgrenciId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Ogrenciler_OgrenciId",
                table: "AspNetUsers",
                column: "OgrenciId",
                principalTable: "Ogrenciler",
                principalColumn: "OgrenciId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Ogrenciler_OgrenciId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_OgrenciId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "OgrenciId",
                table: "AspNetUsers");
        }
    }
}
