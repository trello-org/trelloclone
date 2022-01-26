using Microsoft.EntityFrameworkCore.Migrations;

namespace TrelloClone.Migrations
{
    public partial class TableNameUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Labels",
                table: "Labels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cards",
                table: "Cards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CardLists",
                table: "CardLists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Boards",
                table: "Boards");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "users");

            migrationBuilder.RenameTable(
                name: "Labels",
                newName: "labels");

            migrationBuilder.RenameTable(
                name: "Cards",
                newName: "cards");

            migrationBuilder.RenameTable(
                name: "CardLists",
                newName: "cardlists");

            migrationBuilder.RenameTable(
                name: "Boards",
                newName: "boards");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Username",
                table: "users",
                newName: "IX_users_Username");

            migrationBuilder.RenameIndex(
                name: "IX_Labels_CardId",
                table: "labels",
                newName: "IX_labels_CardId");

            migrationBuilder.RenameIndex(
                name: "IX_Cards_CardListId",
                table: "cards",
                newName: "IX_cards_CardListId");

            migrationBuilder.RenameIndex(
                name: "IX_CardLists_BoardId",
                table: "cardlists",
                newName: "IX_cardlists_BoardId");

            migrationBuilder.RenameIndex(
                name: "IX_Boards_UserId",
                table: "boards",
                newName: "IX_boards_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_labels",
                table: "labels",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cards",
                table: "cards",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cardlists",
                table: "cardlists",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_boards",
                table: "boards",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_boards_users_UserId",
                table: "boards",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_cardlists_boards_BoardId",
                table: "cardlists",
                column: "BoardId",
                principalTable: "boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_cards_cardlists_CardListId",
                table: "cards",
                column: "CardListId",
                principalTable: "cardlists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_labels_cards_CardId",
                table: "labels",
                column: "CardId",
                principalTable: "cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_boards_users_UserId",
                table: "boards");

            migrationBuilder.DropForeignKey(
                name: "FK_cardlists_boards_BoardId",
                table: "cardlists");

            migrationBuilder.DropForeignKey(
                name: "FK_cards_cardlists_CardListId",
                table: "cards");

            migrationBuilder.DropForeignKey(
                name: "FK_labels_cards_CardId",
                table: "labels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_labels",
                table: "labels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cards",
                table: "cards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cardlists",
                table: "cardlists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_boards",
                table: "boards");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "labels",
                newName: "Labels");

            migrationBuilder.RenameTable(
                name: "cards",
                newName: "Cards");

            migrationBuilder.RenameTable(
                name: "cardlists",
                newName: "CardLists");

            migrationBuilder.RenameTable(
                name: "boards",
                newName: "Boards");

            migrationBuilder.RenameIndex(
                name: "IX_users_Username",
                table: "Users",
                newName: "IX_Users_Username");

            migrationBuilder.RenameIndex(
                name: "IX_labels_CardId",
                table: "Labels",
                newName: "IX_Labels_CardId");

            migrationBuilder.RenameIndex(
                name: "IX_cards_CardListId",
                table: "Cards",
                newName: "IX_Cards_CardListId");

            migrationBuilder.RenameIndex(
                name: "IX_cardlists_BoardId",
                table: "CardLists",
                newName: "IX_CardLists_BoardId");

            migrationBuilder.RenameIndex(
                name: "IX_boards_UserId",
                table: "Boards",
                newName: "IX_Boards_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Labels",
                table: "Labels",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cards",
                table: "Cards",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CardLists",
                table: "CardLists",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Boards",
                table: "Boards",
                column: "Id");

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
    }
}
