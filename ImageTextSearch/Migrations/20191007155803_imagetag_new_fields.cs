using Microsoft.EntityFrameworkCore.Migrations;

namespace ImageTextSearch.Migrations
{
    public partial class imagetag_new_fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "MaxX",
                table: "ImageTag",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MaxY",
                table: "ImageTag",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MinX",
                table: "ImageTag",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MinY",
                table: "ImageTag",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Score",
                table: "ImageTag",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxX",
                table: "ImageTag");

            migrationBuilder.DropColumn(
                name: "MaxY",
                table: "ImageTag");

            migrationBuilder.DropColumn(
                name: "MinX",
                table: "ImageTag");

            migrationBuilder.DropColumn(
                name: "MinY",
                table: "ImageTag");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "ImageTag");
        }
    }
}
