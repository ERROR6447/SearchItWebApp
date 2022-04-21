using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SearchIt.DataAccess.Migrations
{
    public partial class AddedForeignKeyToBooKmark : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_BookMarks_PostId",
                table: "BookMarks",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookMarks_Postings_PostId",
                table: "BookMarks",
                column: "PostId",
                principalTable: "Postings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookMarks_Postings_PostId",
                table: "BookMarks");

            migrationBuilder.DropIndex(
                name: "IX_BookMarks_PostId",
                table: "BookMarks");
        }
    }
}
