using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAppUser.Migrations
{
    public partial class seagregoelcampocontrollerallog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Controller",
                table: "ActivityLog",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Controller",
                table: "ActivityLog");
        }
    }
}
