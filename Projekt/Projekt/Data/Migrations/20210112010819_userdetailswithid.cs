using Microsoft.EntityFrameworkCore.Migrations;

namespace Projekt.Data.Migrations
{
    public partial class userdetailswithid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserDetails",
                columns: table => new
                {
                    detailsId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    pesel = table.Column<string>(nullable: true),
                    dateOfBirth = table.Column<string>(nullable: true),
                    userName = table.Column<string>(maxLength: 100, nullable: true),
                    authorFK = table.Column<string>(nullable: true)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserDetails");
        }
    }
}
