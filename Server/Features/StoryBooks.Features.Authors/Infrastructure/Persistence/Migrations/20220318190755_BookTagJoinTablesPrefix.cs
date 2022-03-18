using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoryBooks.Features.Authors.Infrastructure.Persistence.Migrations
{
    public partial class BookTagJoinTablesPrefix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookTag_Authors_Book_BooksId",
                table: "BookTag");

            migrationBuilder.DropForeignKey(
                name: "FK_BookTag_Authors_Tag_TagsId",
                table: "BookTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookTag",
                table: "BookTag");

            migrationBuilder.RenameTable(
                name: "BookTag",
                newName: "Authors_Book_Tag");

            migrationBuilder.RenameIndex(
                name: "IX_BookTag_TagsId",
                table: "Authors_Book_Tag",
                newName: "IX_Authors_Book_Tag_TagsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Authors_Book_Tag",
                table: "Authors_Book_Tag",
                columns: new[] { "BooksId", "TagsId" });

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Book_Tag_Authors_Book_BooksId",
                table: "Authors_Book_Tag");

            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Book_Tag_Authors_Tag_TagsId",
                table: "Authors_Book_Tag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Authors_Book_Tag",
                table: "Authors_Book_Tag");

            migrationBuilder.RenameTable(
                name: "Authors_Book_Tag",
                newName: "BookTag");

            migrationBuilder.RenameIndex(
                name: "IX_Authors_Book_Tag_TagsId",
                table: "BookTag",
                newName: "IX_BookTag_TagsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookTag",
                table: "BookTag",
                columns: new[] { "BooksId", "TagsId" });

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
    }
}
