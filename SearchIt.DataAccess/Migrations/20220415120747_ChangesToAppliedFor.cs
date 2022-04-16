using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SearchIt.DataAccess.Migrations
{
    public partial class ChangesToAppliedFor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplyStatus",
                table: "AppliedFors",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplyStatus",
                table: "AppliedFors");
        }
    }
}
