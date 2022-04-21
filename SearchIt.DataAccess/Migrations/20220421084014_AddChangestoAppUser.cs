using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SearchIt.DataAccess.Migrations
{
    public partial class AddChangestoAppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "KeySkills",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KeySkills",
                table: "AspNetUsers");
        }
    }
}
