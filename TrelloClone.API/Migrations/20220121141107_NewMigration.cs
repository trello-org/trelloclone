using Microsoft.EntityFrameworkCore.Migrations;

namespace TrelloClone.Migrations
{
    public partial class NewMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "users",
                newName: "username");

            migrationBuilder.RenameIndex(
                name: "IX_users_Username",
                table: "users",
                newName: "IX_users_username");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "username",
                table: "users",
                newName: "Username");

            migrationBuilder.RenameIndex(
                name: "IX_users_username",
                table: "users",
                newName: "IX_users_Username");
        }
    }
}
