using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoryBooks.Features.Authors.Infrastructure.Persistence.Migrations
{
    public partial class ChangeTableNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BattleStat_Battles_BattlesId",
                table: "BattleStat");

            migrationBuilder.DropForeignKey(
                name: "FK_BattleStat_Stats_IncludedStatsId",
                table: "BattleStat");

            migrationBuilder.DropForeignKey(
                name: "FK_BookGenre_Books_BooksId",
                table: "BookGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_BookGenre_Genres_GenresId",
                table: "BookGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Authors_AuthorId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_BookCovers_CoverId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_BookTag_Books_BooksId",
                table: "BookTag");

            migrationBuilder.DropForeignKey(
                name: "FK_BookTag_Tags_TagsId",
                table: "BookTag");

            migrationBuilder.DropForeignKey(
                name: "FK_Chapters_Battles_BattleId",
                table: "Chapters");

            migrationBuilder.DropForeignKey(
                name: "FK_Chapters_Books_BookId",
                table: "Chapters");

            migrationBuilder.DropForeignKey(
                name: "FK_Chapters_Choices_ChoiceId",
                table: "Chapters");

            migrationBuilder.DropForeignKey(
                name: "FK_Choices_Chapters_NextChapterId",
                table: "Choices");

            migrationBuilder.DropForeignKey(
                name: "FK_Effects_Chapters_ChapterId",
                table: "Effects");

            migrationBuilder.DropForeignKey(
                name: "FK_StatChanges_Effects_EffectId",
                table: "StatChanges");

            migrationBuilder.DropForeignKey(
                name: "FK_StatChanges_Stats_StatId",
                table: "StatChanges");

            migrationBuilder.DropForeignKey(
                name: "FK_Stats_Books_BookId",
                table: "Stats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stats",
                table: "Stats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StatChanges",
                table: "StatChanges");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Genres",
                table: "Genres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Effects",
                table: "Effects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Choices",
                table: "Choices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chapters",
                table: "Chapters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Books",
                table: "Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookCovers",
                table: "BookCovers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Battles",
                table: "Battles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Authors",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "FirstChapterId",
                table: "Books");

            migrationBuilder.RenameTable(
                name: "Tags",
                newName: "Authors_Tag");

            migrationBuilder.RenameTable(
                name: "Stats",
                newName: "Authors_Stat");

            migrationBuilder.RenameTable(
                name: "StatChanges",
                newName: "Authors_StatModifier");

            migrationBuilder.RenameTable(
                name: "Genres",
                newName: "Authors_Genre");

            migrationBuilder.RenameTable(
                name: "Effects",
                newName: "Authors_Effect");

            migrationBuilder.RenameTable(
                name: "Choices",
                newName: "Authors_Choice");

            migrationBuilder.RenameTable(
                name: "Chapters",
                newName: "Authors_Chapter");

            migrationBuilder.RenameTable(
                name: "Books",
                newName: "Authors_Book");

            migrationBuilder.RenameTable(
                name: "BookCovers",
                newName: "Authors_BookCover");

            migrationBuilder.RenameTable(
                name: "Battles",
                newName: "Authors_Battle");

            migrationBuilder.RenameTable(
                name: "Authors",
                newName: "Authors_Author");

            migrationBuilder.RenameIndex(
                name: "IX_Stats_BookId",
                table: "Authors_Stat",
                newName: "IX_Authors_Stat_BookId");

            migrationBuilder.RenameIndex(
                name: "IX_StatChanges_StatId",
                table: "Authors_StatModifier",
                newName: "IX_Authors_StatModifier_StatId");

            migrationBuilder.RenameIndex(
                name: "IX_StatChanges_EffectId",
                table: "Authors_StatModifier",
                newName: "IX_Authors_StatModifier_EffectId");

            migrationBuilder.RenameIndex(
                name: "IX_Effects_ChapterId",
                table: "Authors_Effect",
                newName: "IX_Authors_Effect_ChapterId");

            migrationBuilder.RenameIndex(
                name: "IX_Choices_NextChapterId",
                table: "Authors_Choice",
                newName: "IX_Authors_Choice_NextChapterId");

            migrationBuilder.RenameIndex(
                name: "IX_Chapters_ChoiceId",
                table: "Authors_Chapter",
                newName: "IX_Authors_Chapter_ChoiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Chapters_BookId",
                table: "Authors_Chapter",
                newName: "IX_Authors_Chapter_BookId");

            migrationBuilder.RenameIndex(
                name: "IX_Chapters_BattleId",
                table: "Authors_Chapter",
                newName: "IX_Authors_Chapter_BattleId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_CoverId",
                table: "Authors_Book",
                newName: "IX_Authors_Book_CoverId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_AuthorId",
                table: "Authors_Book",
                newName: "IX_Authors_Book_AuthorId");

            migrationBuilder.AddColumn<bool>(
                name: "IsFirstChapter",
                table: "Authors_Chapter",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Authors_Tag",
                table: "Authors_Tag",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Authors_Stat",
                table: "Authors_Stat",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Authors_StatModifier",
                table: "Authors_StatModifier",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Authors_Genre",
                table: "Authors_Genre",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Authors_Effect",
                table: "Authors_Effect",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Authors_Choice",
                table: "Authors_Choice",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Authors_Chapter",
                table: "Authors_Chapter",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Authors_Book",
                table: "Authors_Book",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Authors_BookCover",
                table: "Authors_BookCover",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Authors_Battle",
                table: "Authors_Battle",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Authors_Author",
                table: "Authors_Author",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Book_Authors_Author_AuthorId",
                table: "Authors_Book",
                column: "AuthorId",
                principalTable: "Authors_Author",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Book_Authors_BookCover_CoverId",
                table: "Authors_Book",
                column: "CoverId",
                principalTable: "Authors_BookCover",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Chapter_Authors_Battle_BattleId",
                table: "Authors_Chapter",
                column: "BattleId",
                principalTable: "Authors_Battle",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Chapter_Authors_Book_BookId",
                table: "Authors_Chapter",
                column: "BookId",
                principalTable: "Authors_Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Chapter_Authors_Choice_ChoiceId",
                table: "Authors_Chapter",
                column: "ChoiceId",
                principalTable: "Authors_Choice",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Choice_Authors_Chapter_NextChapterId",
                table: "Authors_Choice",
                column: "NextChapterId",
                principalTable: "Authors_Chapter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Effect_Authors_Chapter_ChapterId",
                table: "Authors_Effect",
                column: "ChapterId",
                principalTable: "Authors_Chapter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Stat_Authors_Book_BookId",
                table: "Authors_Stat",
                column: "BookId",
                principalTable: "Authors_Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_StatModifier_Authors_Effect_EffectId",
                table: "Authors_StatModifier",
                column: "EffectId",
                principalTable: "Authors_Effect",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_StatModifier_Authors_Stat_StatId",
                table: "Authors_StatModifier",
                column: "StatId",
                principalTable: "Authors_Stat",
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

            migrationBuilder.AddForeignKey(
                name: "FK_BookTag_Authors_Book_BooksId",
                table: "BookTag",
                column: "BooksId",
                principalTable: "Authors_Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookTag_Authors_Tag_TagsId",
                table: "BookTag",
                column: "TagsId",
                principalTable: "Authors_Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Book_Authors_Author_AuthorId",
                table: "Authors_Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Book_Authors_BookCover_CoverId",
                table: "Authors_Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Chapter_Authors_Battle_BattleId",
                table: "Authors_Chapter");

            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Chapter_Authors_Book_BookId",
                table: "Authors_Chapter");

            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Chapter_Authors_Choice_ChoiceId",
                table: "Authors_Chapter");

            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Choice_Authors_Chapter_NextChapterId",
                table: "Authors_Choice");

            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Effect_Authors_Chapter_ChapterId",
                table: "Authors_Effect");

            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Stat_Authors_Book_BookId",
                table: "Authors_Stat");

            migrationBuilder.DropForeignKey(
                name: "FK_Authors_StatModifier_Authors_Effect_EffectId",
                table: "Authors_StatModifier");

            migrationBuilder.DropForeignKey(
                name: "FK_Authors_StatModifier_Authors_Stat_StatId",
                table: "Authors_StatModifier");

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

            migrationBuilder.DropForeignKey(
                name: "FK_BookTag_Authors_Book_BooksId",
                table: "BookTag");

            migrationBuilder.DropForeignKey(
                name: "FK_BookTag_Authors_Tag_TagsId",
                table: "BookTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Authors_Tag",
                table: "Authors_Tag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Authors_StatModifier",
                table: "Authors_StatModifier");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Authors_Stat",
                table: "Authors_Stat");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Authors_Genre",
                table: "Authors_Genre");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Authors_Effect",
                table: "Authors_Effect");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Authors_Choice",
                table: "Authors_Choice");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Authors_Chapter",
                table: "Authors_Chapter");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Authors_BookCover",
                table: "Authors_BookCover");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Authors_Book",
                table: "Authors_Book");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Authors_Battle",
                table: "Authors_Battle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Authors_Author",
                table: "Authors_Author");

            migrationBuilder.DropColumn(
                name: "IsFirstChapter",
                table: "Authors_Chapter");

            migrationBuilder.RenameTable(
                name: "Authors_Tag",
                newName: "Tags");

            migrationBuilder.RenameTable(
                name: "Authors_StatModifier",
                newName: "StatChanges");

            migrationBuilder.RenameTable(
                name: "Authors_Stat",
                newName: "Stats");

            migrationBuilder.RenameTable(
                name: "Authors_Genre",
                newName: "Genres");

            migrationBuilder.RenameTable(
                name: "Authors_Effect",
                newName: "Effects");

            migrationBuilder.RenameTable(
                name: "Authors_Choice",
                newName: "Choices");

            migrationBuilder.RenameTable(
                name: "Authors_Chapter",
                newName: "Chapters");

            migrationBuilder.RenameTable(
                name: "Authors_BookCover",
                newName: "BookCovers");

            migrationBuilder.RenameTable(
                name: "Authors_Book",
                newName: "Books");

            migrationBuilder.RenameTable(
                name: "Authors_Battle",
                newName: "Battles");

            migrationBuilder.RenameTable(
                name: "Authors_Author",
                newName: "Authors");

            migrationBuilder.RenameIndex(
                name: "IX_Authors_StatModifier_StatId",
                table: "StatChanges",
                newName: "IX_StatChanges_StatId");

            migrationBuilder.RenameIndex(
                name: "IX_Authors_StatModifier_EffectId",
                table: "StatChanges",
                newName: "IX_StatChanges_EffectId");

            migrationBuilder.RenameIndex(
                name: "IX_Authors_Stat_BookId",
                table: "Stats",
                newName: "IX_Stats_BookId");

            migrationBuilder.RenameIndex(
                name: "IX_Authors_Effect_ChapterId",
                table: "Effects",
                newName: "IX_Effects_ChapterId");

            migrationBuilder.RenameIndex(
                name: "IX_Authors_Choice_NextChapterId",
                table: "Choices",
                newName: "IX_Choices_NextChapterId");

            migrationBuilder.RenameIndex(
                name: "IX_Authors_Chapter_ChoiceId",
                table: "Chapters",
                newName: "IX_Chapters_ChoiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Authors_Chapter_BookId",
                table: "Chapters",
                newName: "IX_Chapters_BookId");

            migrationBuilder.RenameIndex(
                name: "IX_Authors_Chapter_BattleId",
                table: "Chapters",
                newName: "IX_Chapters_BattleId");

            migrationBuilder.RenameIndex(
                name: "IX_Authors_Book_CoverId",
                table: "Books",
                newName: "IX_Books_CoverId");

            migrationBuilder.RenameIndex(
                name: "IX_Authors_Book_AuthorId",
                table: "Books",
                newName: "IX_Books_AuthorId");

            migrationBuilder.AddColumn<string>(
                name: "FirstChapterId",
                table: "Books",
                type: "nvarchar(38)",
                maxLength: 38,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StatChanges",
                table: "StatChanges",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stats",
                table: "Stats",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genres",
                table: "Genres",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Effects",
                table: "Effects",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Choices",
                table: "Choices",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chapters",
                table: "Chapters",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookCovers",
                table: "BookCovers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Books",
                table: "Books",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Battles",
                table: "Battles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Authors",
                table: "Authors",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BattleStat_Battles_BattlesId",
                table: "BattleStat",
                column: "BattlesId",
                principalTable: "Battles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BattleStat_Stats_IncludedStatsId",
                table: "BattleStat",
                column: "IncludedStatsId",
                principalTable: "Stats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookGenre_Books_BooksId",
                table: "BookGenre",
                column: "BooksId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookGenre_Genres_GenresId",
                table: "BookGenre",
                column: "GenresId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Authors_AuthorId",
                table: "Books",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BookCovers_CoverId",
                table: "Books",
                column: "CoverId",
                principalTable: "BookCovers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookTag_Books_BooksId",
                table: "BookTag",
                column: "BooksId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookTag_Tags_TagsId",
                table: "BookTag",
                column: "TagsId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Chapters_Battles_BattleId",
                table: "Chapters",
                column: "BattleId",
                principalTable: "Battles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Chapters_Books_BookId",
                table: "Chapters",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Chapters_Choices_ChoiceId",
                table: "Chapters",
                column: "ChoiceId",
                principalTable: "Choices",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Choices_Chapters_NextChapterId",
                table: "Choices",
                column: "NextChapterId",
                principalTable: "Chapters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Effects_Chapters_ChapterId",
                table: "Effects",
                column: "ChapterId",
                principalTable: "Chapters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StatChanges_Effects_EffectId",
                table: "StatChanges",
                column: "EffectId",
                principalTable: "Effects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StatChanges_Stats_StatId",
                table: "StatChanges",
                column: "StatId",
                principalTable: "Stats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stats_Books_BookId",
                table: "Stats",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
