using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoryBooks.Features.Authors.Infrastructure.Persistence.Migrations
{
    public partial class JoinTablesPrefixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Book_Tag_Authors_Book_BooksId",
                table: "Authors_Book_Tag");

            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Book_Tag_Authors_Tag_TagsId",
                table: "Authors_Book_Tag");

            migrationBuilder.DropForeignKey(
                name: "FK_BattleStat_Authors_Battle_BattlesId",
                table: "BattleStat");

            migrationBuilder.DropForeignKey(
                name: "FK_BattleStat_Authors_Stat_IncludedStatsId",
                table: "BattleStat");

            migrationBuilder.DropForeignKey(
                name: "FK_BookGenre_Authors_Book_BooksId",
                table: "BookGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_BookGenre_Authors_Genre_GenresId",
                table: "BookGenre");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookGenre",
                table: "BookGenre");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BattleStat",
                table: "BattleStat");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Authors_Book_Tag",
                table: "Authors_Book_Tag");

            migrationBuilder.RenameTable(
                name: "BookGenre",
                newName: "Authors_BookGenre");

            migrationBuilder.RenameTable(
                name: "BattleStat",
                newName: "Authors_BattleStat");

            migrationBuilder.RenameTable(
                name: "Authors_Book_Tag",
                newName: "Authors_BookTag");

            migrationBuilder.RenameIndex(
                name: "IX_BookGenre_GenresId",
                table: "Authors_BookGenre",
                newName: "IX_Authors_BookGenre_GenresId");

            migrationBuilder.RenameIndex(
                name: "IX_BattleStat_IncludedStatsId",
                table: "Authors_BattleStat",
                newName: "IX_Authors_BattleStat_IncludedStatsId");

            migrationBuilder.RenameIndex(
                name: "IX_Authors_Book_Tag_TagsId",
                table: "Authors_BookTag",
                newName: "IX_Authors_BookTag_TagsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Authors_BookGenre",
                table: "Authors_BookGenre",
                columns: new[] { "BooksId", "GenresId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Authors_BattleStat",
                table: "Authors_BattleStat",
                columns: new[] { "BattlesId", "IncludedStatsId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Authors_BookTag",
                table: "Authors_BookTag",
                columns: new[] { "BooksId", "TagsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_BattleStat_Authors_Battle_BattlesId",
                table: "Authors_BattleStat",
                column: "BattlesId",
                principalTable: "Authors_Battle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_BattleStat_Authors_Stat_IncludedStatsId",
                table: "Authors_BattleStat",
                column: "IncludedStatsId",
                principalTable: "Authors_Stat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_BookGenre_Authors_Book_BooksId",
                table: "Authors_BookGenre",
                column: "BooksId",
                principalTable: "Authors_Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_BookGenre_Authors_Genre_GenresId",
                table: "Authors_BookGenre",
                column: "GenresId",
                principalTable: "Authors_Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_BookTag_Authors_Book_BooksId",
                table: "Authors_BookTag",
                column: "BooksId",
                principalTable: "Authors_Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_BookTag_Authors_Tag_TagsId",
                table: "Authors_BookTag",
                column: "TagsId",
                principalTable: "Authors_Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_BattleStat_Authors_Battle_BattlesId",
                table: "Authors_BattleStat");

            migrationBuilder.DropForeignKey(
                name: "FK_Authors_BattleStat_Authors_Stat_IncludedStatsId",
                table: "Authors_BattleStat");

            migrationBuilder.DropForeignKey(
                name: "FK_Authors_BookGenre_Authors_Book_BooksId",
                table: "Authors_BookGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_Authors_BookGenre_Authors_Genre_GenresId",
                table: "Authors_BookGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_Authors_BookTag_Authors_Book_BooksId",
                table: "Authors_BookTag");

            migrationBuilder.DropForeignKey(
                name: "FK_Authors_BookTag_Authors_Tag_TagsId",
                table: "Authors_BookTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Authors_BookTag",
                table: "Authors_BookTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Authors_BookGenre",
                table: "Authors_BookGenre");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Authors_BattleStat",
                table: "Authors_BattleStat");

            migrationBuilder.RenameTable(
                name: "Authors_BookTag",
                newName: "Authors_Book_Tag");

            migrationBuilder.RenameTable(
                name: "Authors_BookGenre",
                newName: "BookGenre");

            migrationBuilder.RenameTable(
                name: "Authors_BattleStat",
                newName: "BattleStat");

            migrationBuilder.RenameIndex(
                name: "IX_Authors_BookTag_TagsId",
                table: "Authors_Book_Tag",
                newName: "IX_Authors_Book_Tag_TagsId");

            migrationBuilder.RenameIndex(
                name: "IX_Authors_BookGenre_GenresId",
                table: "BookGenre",
                newName: "IX_BookGenre_GenresId");

            migrationBuilder.RenameIndex(
                name: "IX_Authors_BattleStat_IncludedStatsId",
                table: "BattleStat",
                newName: "IX_BattleStat_IncludedStatsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Authors_Book_Tag",
                table: "Authors_Book_Tag",
                columns: new[] { "BooksId", "TagsId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookGenre",
                table: "BookGenre",
                columns: new[] { "BooksId", "GenresId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_BattleStat",
                table: "BattleStat",
                columns: new[] { "BattlesId", "IncludedStatsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Book_Tag_Authors_Book_BooksId",
                table: "Authors_Book_Tag",
                column: "BooksId",
                principalTable: "Authors_Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Book_Tag_Authors_Tag_TagsId",
                table: "Authors_Book_Tag",
                column: "TagsId",
                principalTable: "Authors_Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BattleStat_Authors_Battle_BattlesId",
                table: "BattleStat",
                column: "BattlesId",
                principalTable: "Authors_Battle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BattleStat_Authors_Stat_IncludedStatsId",
                table: "BattleStat",
                column: "IncludedStatsId",
                principalTable: "Authors_Stat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookGenre_Authors_Book_BooksId",
                table: "BookGenre",
                column: "BooksId",
                principalTable: "Authors_Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookGenre_Authors_Genre_GenresId",
                table: "BookGenre",
                column: "GenresId",
                principalTable: "Authors_Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
