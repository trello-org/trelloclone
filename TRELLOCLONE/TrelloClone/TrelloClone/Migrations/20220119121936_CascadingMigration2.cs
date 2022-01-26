using Microsoft.EntityFrameworkCore.Migrations;

namespace TrelloClone.Migrations
{
    public partial class CascadingMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardLists_Boards_BelongsToId",
                table: "CardLists");

            migrationBuilder.DropForeignKey(
                name: "FK_Labels_Cards_CardId",
                table: "Labels");

            migrationBuilder.RenameColumn(
                name: "CardId",
                table: "Labels",
                newName: "BelongsToCardId");

            migrationBuilder.RenameIndex(
                name: "IX_Labels_CardId",
                table: "Labels",
                newName: "IX_Labels_BelongsToCardId");

            migrationBuilder.RenameColumn(
                name: "BelongsToId",
                table: "CardLists",
                newName: "BelongsToBoardId");

            migrationBuilder.RenameIndex(
                name: "IX_CardLists_BelongsToId",
                table: "CardLists",
                newName: "IX_CardLists_BelongsToBoardId");

            migrationBuilder.AddForeignKey(
                name: "FK_CardLists_Boards_BelongsToBoardId",
                table: "CardLists",
                column: "BelongsToBoardId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Labels_Cards_BelongsToCardId",
                table: "Labels",
                column: "BelongsToCardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardLists_Boards_BelongsToBoardId",
                table: "CardLists");

            migrationBuilder.DropForeignKey(
                name: "FK_Labels_Cards_BelongsToCardId",
                table: "Labels");

            migrationBuilder.RenameColumn(
                name: "BelongsToCardId",
                table: "Labels",
                newName: "CardId");

            migrationBuilder.RenameIndex(
                name: "IX_Labels_BelongsToCardId",
                table: "Labels",
                newName: "IX_Labels_CardId");

            migrationBuilder.RenameColumn(
                name: "BelongsToBoardId",
                table: "CardLists",
                newName: "BelongsToId");

            migrationBuilder.RenameIndex(
                name: "IX_CardLists_BelongsToBoardId",
                table: "CardLists",
                newName: "IX_CardLists_BelongsToId");

            migrationBuilder.AddForeignKey(
                name: "FK_CardLists_Boards_BelongsToId",
                table: "CardLists",
                column: "BelongsToId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Labels_Cards_CardId",
                table: "Labels",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
