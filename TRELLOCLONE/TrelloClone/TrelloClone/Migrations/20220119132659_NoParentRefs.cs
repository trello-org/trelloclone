using Microsoft.EntityFrameworkCore.Migrations;

namespace TrelloClone.Migrations
{
    public partial class NoParentRefs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boards_Users_BoardOwnerId",
                table: "Boards");

            migrationBuilder.DropForeignKey(
                name: "FK_CardLists_Boards_BelongsToBoardId",
                table: "CardLists");

            migrationBuilder.DropForeignKey(
                name: "FK_Cards_CardLists_BelongsToCardListId",
                table: "Cards");

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
                name: "BelongsToCardListId",
                table: "Cards",
                newName: "CardListId");

            migrationBuilder.RenameIndex(
                name: "IX_Cards_BelongsToCardListId",
                table: "Cards",
                newName: "IX_Cards_CardListId");

            migrationBuilder.RenameColumn(
                name: "BelongsToBoardId",
                table: "CardLists",
                newName: "BoardId");

            migrationBuilder.RenameIndex(
                name: "IX_CardLists_BelongsToBoardId",
                table: "CardLists",
                newName: "IX_CardLists_BoardId");

            migrationBuilder.RenameColumn(
                name: "BoardOwnerId",
                table: "Boards",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Boards_BoardOwnerId",
                table: "Boards",
                newName: "IX_Boards_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Boards_Users_UserId",
                table: "Boards",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CardLists_Boards_BoardId",
                table: "CardLists",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_CardLists_CardListId",
                table: "Cards",
                column: "CardListId",
                principalTable: "CardLists",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boards_Users_UserId",
                table: "Boards");

            migrationBuilder.DropForeignKey(
                name: "FK_CardLists_Boards_BoardId",
                table: "CardLists");

            migrationBuilder.DropForeignKey(
                name: "FK_Cards_CardLists_CardListId",
                table: "Cards");

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
                name: "CardListId",
                table: "Cards",
                newName: "BelongsToCardListId");

            migrationBuilder.RenameIndex(
                name: "IX_Cards_CardListId",
                table: "Cards",
                newName: "IX_Cards_BelongsToCardListId");

            migrationBuilder.RenameColumn(
                name: "BoardId",
                table: "CardLists",
                newName: "BelongsToBoardId");

            migrationBuilder.RenameIndex(
                name: "IX_CardLists_BoardId",
                table: "CardLists",
                newName: "IX_CardLists_BelongsToBoardId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Boards",
                newName: "BoardOwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Boards_UserId",
                table: "Boards",
                newName: "IX_Boards_BoardOwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Boards_Users_BoardOwnerId",
                table: "Boards",
                column: "BoardOwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CardLists_Boards_BelongsToBoardId",
                table: "CardLists",
                column: "BelongsToBoardId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_CardLists_BelongsToCardListId",
                table: "Cards",
                column: "BelongsToCardListId",
                principalTable: "CardLists",
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
    }
}
