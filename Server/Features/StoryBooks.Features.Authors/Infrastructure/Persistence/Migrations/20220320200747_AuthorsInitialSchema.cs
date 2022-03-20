using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoryBooks.Features.Authors.Infrastructure.Persistence.Migrations
{
    public partial class AuthorsInitialSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors_Author",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(38)", maxLength: 38, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Alias = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors_Author", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Authors_Battle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnemyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DiceBattle = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors_Battle", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Authors_BookCover",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Content = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors_BookCover", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Authors_Genre",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors_Genre", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Authors_Tag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Authors_Book",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CoverId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status_Value = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors_Book", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Authors_Book_Authors_Author_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors_Author",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Authors_Book_Authors_BookCover_CoverId",
                        column: x => x.CoverId,
                        principalTable: "Authors_BookCover",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Authors_BookGenre",
                columns: table => new
                {
                    BooksId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GenresId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors_BookGenre", x => new { x.BooksId, x.GenresId });
                    table.ForeignKey(
                        name: "FK_Authors_BookGenre_Authors_Book_BooksId",
                        column: x => x.BooksId,
                        principalTable: "Authors_Book",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Authors_BookGenre_Authors_Genre_GenresId",
                        column: x => x.GenresId,
                        principalTable: "Authors_Genre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Authors_BookTag",
                columns: table => new
                {
                    BooksId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors_BookTag", x => new { x.BooksId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_Authors_BookTag_Authors_Book_BooksId",
                        column: x => x.BooksId,
                        principalTable: "Authors_Book",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Authors_BookTag_Authors_Tag_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Authors_Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Authors_Stat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    IsCritical = table.Column<bool>(type: "bit", nullable: false),
                    MinimumValue = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    GroupName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BookId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors_Stat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Authors_Stat_Authors_Book_BookId",
                        column: x => x.BookId,
                        principalTable: "Authors_Book",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Authors_BattleStat",
                columns: table => new
                {
                    BattlesId = table.Column<int>(type: "int", nullable: false),
                    IncludedStatsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors_BattleStat", x => new { x.BattlesId, x.IncludedStatsId });
                    table.ForeignKey(
                        name: "FK_Authors_BattleStat_Authors_Battle_BattlesId",
                        column: x => x.BattlesId,
                        principalTable: "Authors_Battle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Authors_BattleStat_Authors_Stat_IncludedStatsId",
                        column: x => x.IncludedStatsId,
                        principalTable: "Authors_Stat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Authors_Chapter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsFirstChapter = table.Column<bool>(type: "bit", nullable: false),
                    IsCheckpoint = table.Column<bool>(type: "bit", nullable: false),
                    IsDiceChoice = table.Column<bool>(type: "bit", nullable: false),
                    BookId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BattleId = table.Column<int>(type: "int", nullable: true),
                    ChoiceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors_Chapter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Authors_Chapter_Authors_Battle_BattleId",
                        column: x => x.BattleId,
                        principalTable: "Authors_Battle",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Authors_Chapter_Authors_Book_BookId",
                        column: x => x.BookId,
                        principalTable: "Authors_Book",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Authors_Choice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    NextChapterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors_Choice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Authors_Choice_Authors_Chapter_NextChapterId",
                        column: x => x.NextChapterId,
                        principalTable: "Authors_Chapter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Authors_Effect",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ChapterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors_Effect", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Authors_Effect_Authors_Chapter_ChapterId",
                        column: x => x.ChapterId,
                        principalTable: "Authors_Chapter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Authors_StatModifier",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatId = table.Column<int>(type: "int", nullable: false),
                    Change = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    EffectId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors_StatModifier", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Authors_StatModifier_Authors_Effect_EffectId",
                        column: x => x.EffectId,
                        principalTable: "Authors_Effect",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Authors_StatModifier_Authors_Stat_StatId",
                        column: x => x.StatId,
                        principalTable: "Authors_Stat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Authors_BattleStat_IncludedStatsId",
                table: "Authors_BattleStat",
                column: "IncludedStatsId");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_Book_AuthorId",
                table: "Authors_Book",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_Book_CoverId",
                table: "Authors_Book",
                column: "CoverId");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_BookGenre_GenresId",
                table: "Authors_BookGenre",
                column: "GenresId");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_BookTag_TagsId",
                table: "Authors_BookTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_Chapter_BattleId",
                table: "Authors_Chapter",
                column: "BattleId");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_Chapter_BookId",
                table: "Authors_Chapter",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_Chapter_ChoiceId",
                table: "Authors_Chapter",
                column: "ChoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_Choice_NextChapterId",
                table: "Authors_Choice",
                column: "NextChapterId");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_Effect_ChapterId",
                table: "Authors_Effect",
                column: "ChapterId");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_Stat_BookId",
                table: "Authors_Stat",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_StatModifier_EffectId",
                table: "Authors_StatModifier",
                column: "EffectId");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_StatModifier_StatId",
                table: "Authors_StatModifier",
                column: "StatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Chapter_Authors_Choice_ChoiceId",
                table: "Authors_Chapter",
                column: "ChoiceId",
                principalTable: "Authors_Choice",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Chapter_Authors_Battle_BattleId",
                table: "Authors_Chapter");

            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Book_Authors_Author_AuthorId",
                table: "Authors_Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Book_Authors_BookCover_CoverId",
                table: "Authors_Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Chapter_Authors_Book_BookId",
                table: "Authors_Chapter");

            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Chapter_Authors_Choice_ChoiceId",
                table: "Authors_Chapter");

            migrationBuilder.DropTable(
                name: "Authors_BattleStat");

            migrationBuilder.DropTable(
                name: "Authors_BookGenre");

            migrationBuilder.DropTable(
                name: "Authors_BookTag");

            migrationBuilder.DropTable(
                name: "Authors_StatModifier");

            migrationBuilder.DropTable(
                name: "Authors_Genre");

            migrationBuilder.DropTable(
                name: "Authors_Tag");

            migrationBuilder.DropTable(
                name: "Authors_Effect");

            migrationBuilder.DropTable(
                name: "Authors_Stat");

            migrationBuilder.DropTable(
                name: "Authors_Battle");

            migrationBuilder.DropTable(
                name: "Authors_Author");

            migrationBuilder.DropTable(
                name: "Authors_BookCover");

            migrationBuilder.DropTable(
                name: "Authors_Book");

            migrationBuilder.DropTable(
                name: "Authors_Choice");

            migrationBuilder.DropTable(
                name: "Authors_Chapter");
        }
    }
}
