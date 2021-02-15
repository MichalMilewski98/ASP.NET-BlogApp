using Microsoft.EntityFrameworkCore.Migrations;

namespace Projekt.Data.Migrations
{
    public partial class PostCommentv2Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "authorFK",
                table: "Comment",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comment_authorFK",
                table: "Comment",
                column: "authorFK");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_AspNetUsers_authorFK",
                table: "Comment",
                column: "authorFK",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_AspNetUsers_authorFK",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Comment_authorFK",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "authorFK",
                table: "Comment");
        }
    }
}
