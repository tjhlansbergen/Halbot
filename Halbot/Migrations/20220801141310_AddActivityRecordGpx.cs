using Microsoft.EntityFrameworkCore.Migrations;

namespace Halbot.Migrations
{
    public partial class AddActivityRecordGpx : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Gpx",
                table: "ActivityRecords",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gpx",
                table: "ActivityRecords");
        }
    }
}
