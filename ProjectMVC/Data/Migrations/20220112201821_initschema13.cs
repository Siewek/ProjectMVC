using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectMVC.Data.Migrations
{
    public partial class initschema13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "fulfilled",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "penalty",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "returned",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "returnedInTime",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "fulfilled",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "penalty",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "returned",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "returnedInTime",
                table: "Orders");
        }
    }
}
