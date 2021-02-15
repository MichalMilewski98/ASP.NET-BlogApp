using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Projekt.Data.Migrations
{
    public partial class UserBlog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserDetails");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "dateOfBirth",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "pesel",
                table: "AspNetUsers",
                maxLength: 11,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "dateOfBirth",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "pesel",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "UserDetails",
                columns: table => new
                {
                    detailsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    authorFK = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    dateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    pesel = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    userName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDetails", x => x.detailsId);
                    table.ForeignKey(
                        name: "FK_UserDetails_AspNetUsers_authorFK",
                        column: x => x.authorFK,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserDetails_authorFK",
                table: "UserDetails",
                column: "authorFK");
        }
    }
}
