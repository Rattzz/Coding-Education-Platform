using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace efcoreApp.Migrations
{
    /// <inheritdoc />
    public partial class VideoWatchedByUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Watched",
                table: "Videos");

            migrationBuilder.CreateTable(
                name: "VideoWatchedChecks",
                columns: table => new
                {
                    VideoWatchedCheckId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OgrenciId = table.Column<string>(type: "TEXT", nullable: true),
                    VideoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Watched = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoWatchedChecks", x => x.VideoWatchedCheckId);
                    table.ForeignKey(
                        name: "FK_VideoWatchedChecks_Ogrenciler_OgrenciId",
                        column: x => x.OgrenciId,
                        principalTable: "Ogrenciler",
                        principalColumn: "OgrenciId");
                    table.ForeignKey(
                        name: "FK_VideoWatchedChecks_Videos_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Videos",
                        principalColumn: "VideoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VideoWatchedChecks_OgrenciId",
                table: "VideoWatchedChecks",
                column: "OgrenciId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoWatchedChecks_VideoId",
                table: "VideoWatchedChecks",
                column: "VideoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VideoWatchedChecks");

            migrationBuilder.AddColumn<bool>(
                name: "Watched",
                table: "Videos",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}
