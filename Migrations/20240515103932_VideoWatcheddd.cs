using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace efcoreApp.Migrations
{
    /// <inheritdoc />
    public partial class VideoWatcheddd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VideoWatchedChecks_Ogrenciler_OgrenciId1",
                table: "VideoWatchedChecks");

            migrationBuilder.DropIndex(
                name: "IX_VideoWatchedChecks_OgrenciId1",
                table: "VideoWatchedChecks");

            migrationBuilder.DropColumn(
                name: "OgrenciId1",
                table: "VideoWatchedChecks");

            migrationBuilder.AlterColumn<string>(
                name: "OgrenciId",
                table: "VideoWatchedChecks",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_VideoWatchedChecks_OgrenciId",
                table: "VideoWatchedChecks",
                column: "OgrenciId");

            migrationBuilder.AddForeignKey(
                name: "FK_VideoWatchedChecks_Ogrenciler_OgrenciId",
                table: "VideoWatchedChecks",
                column: "OgrenciId",
                principalTable: "Ogrenciler",
                principalColumn: "OgrenciId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VideoWatchedChecks_Ogrenciler_OgrenciId",
                table: "VideoWatchedChecks");

            migrationBuilder.DropIndex(
                name: "IX_VideoWatchedChecks_OgrenciId",
                table: "VideoWatchedChecks");

            migrationBuilder.AlterColumn<int>(
                name: "OgrenciId",
                table: "VideoWatchedChecks",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OgrenciId1",
                table: "VideoWatchedChecks",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VideoWatchedChecks_OgrenciId1",
                table: "VideoWatchedChecks",
                column: "OgrenciId1");

            migrationBuilder.AddForeignKey(
                name: "FK_VideoWatchedChecks_Ogrenciler_OgrenciId1",
                table: "VideoWatchedChecks",
                column: "OgrenciId1",
                principalTable: "Ogrenciler",
                principalColumn: "OgrenciId");
        }
    }
}
