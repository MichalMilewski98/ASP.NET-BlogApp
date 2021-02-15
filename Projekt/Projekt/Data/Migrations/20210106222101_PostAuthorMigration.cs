using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Projekt.Data.Migrations
{
    public partial class PostAuthorMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "Post",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "content",
                table: "Post",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "authorFK",
                table: "Post",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "date",
                table: "Post",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Post_authorFK",
                table: "Post",
                column: "authorFK");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_AspNetUsers_authorFK",
                table: "Post",
                column: "authorFK",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_AspNetUsers_authorFK",
                table: "Post");

            migrationBuilder.DropIndex(
                name: "IX_Post_authorFK",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "authorFK",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "date",
                table: "Post");

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "Post",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "content",
                table: "Post",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
