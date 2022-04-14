using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SearchIt.DataAccess.Migrations
{
    public partial class AddChangestoMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TypeOfJob",
                table: "Postings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "AppliedFors",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeOfJob",
                table: "Postings");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AppliedFors");
        }
    }
}
