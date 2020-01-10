using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ImageTextSearch.Migrations
{
    public partial class imagetag_no_composite_key : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageTag_Tags_TagId",
                table: "ImageTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImageTag",
                table: "ImageTag");

            migrationBuilder.AlterColumn<string>(
                name: "TagId",
                table: "ImageTag",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ImageTag",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImageTag",
                table: "ImageTag",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ImageTag_ImageId",
                table: "ImageTag",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageTag_Tags_TagId",
                table: "ImageTag",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Text",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageTag_Tags_TagId",
                table: "ImageTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImageTag",
                table: "ImageTag");

            migrationBuilder.DropIndex(
                name: "IX_ImageTag_ImageId",
                table: "ImageTag");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ImageTag");

            migrationBuilder.AlterColumn<string>(
                name: "TagId",
                table: "ImageTag",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImageTag",
                table: "ImageTag",
                columns: new[] { "ImageId", "TagId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ImageTag_Tags_TagId",
                table: "ImageTag",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Text",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
