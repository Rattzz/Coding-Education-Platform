using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace efcoreApp.Migrations
{
    /// <inheritdoc />
    public partial class Watchss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VideoWatchedChecks_Videos_VideoId",
                table: "VideoWatchedChecks");

            migrationBuilder.DropIndex(
                name: "IX_VideoWatchedChecks_VideoId",
                table: "VideoWatchedChecks");

            migrationBuilder.AddColumn<int>(
                name: "KursId",
                table: "VideoWatchedChecks",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VideoWatchedChecks_KursId",
                table: "VideoWatchedChecks",
                column: "KursId");

            migrationBuilder.AddForeignKey(
                name: "FK_VideoWatchedChecks_Kurslar_KursId",
                table: "VideoWatchedChecks",
                column: "KursId",
                principalTable: "Kurslar",
                principalColumn: "KursId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VideoWatchedChecks_Kurslar_KursId",
                table: "VideoWatchedChecks");

            migrationBuilder.DropIndex(
                name: "IX_VideoWatchedChecks_KursId",
                table: "VideoWatchedChecks");

            migrationBuilder.DropColumn(
                name: "KursId",
                table: "VideoWatchedChecks");

            migrationBuilder.CreateIndex(
                name: "IX_VideoWatchedChecks_VideoId",
                table: "VideoWatchedChecks",
                column: "VideoId");

            migrationBuilder.AddForeignKey(
                name: "FK_VideoWatchedChecks_Videos_VideoId",
                table: "VideoWatchedChecks",
                column: "VideoId",
                principalTable: "Videos",
                principalColumn: "VideoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
