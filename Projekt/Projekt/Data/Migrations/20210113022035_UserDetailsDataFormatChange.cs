using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Projekt.Data.Migrations
{
    public partial class UserDetailsDataFormatChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "userName",
                table: "UserDetails",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<DateTime>(
                name: "dateOfBirth",
                table: "UserDetails",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "userName",
                table: "UserDetails",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "dateOfBirth",
                table: "UserDetails",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime));
        }
    }
}
