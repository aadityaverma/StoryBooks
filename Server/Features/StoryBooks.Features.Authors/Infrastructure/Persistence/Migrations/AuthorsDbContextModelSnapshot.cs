﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StoryBooks.Features.Authors.Infrastructure.Persistence;

#nullable disable

namespace StoryBooks.Features.Authors.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(AuthorsDbContext))]
    partial class AuthorsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BattleStat", b =>
                {
                    b.Property<int>("BattlesId")
                        .HasColumnType("int");

                    b.Property<int>("IncludedStatsId")
                        .HasColumnType("int");

                    b.HasKey("BattlesId", "IncludedStatsId");

                    b.HasIndex("IncludedStatsId");

                    b.ToTable("BattleStat");
                });

            modelBuilder.Entity("BookGenre", b =>
                {
                    b.Property<string>("BooksId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("GenresId")
                        .HasColumnType("int");

                    b.HasKey("BooksId", "GenresId");

                    b.HasIndex("GenresId");

                    b.ToTable("BookGenre");
                });

            modelBuilder.Entity("BookTag", b =>
                {
                    b.Property<string>("BooksId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("TagsId")
                        .HasColumnType("int");

                    b.HasKey("BooksId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("BookTag");
                });

            modelBuilder.Entity("StoryBooks.Features.Authors.Domain.Entities.Author", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Alias")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(38)
                        .HasColumnType("nvarchar(38)");

                    b.HasKey("Id");

                    b.ToTable("Authors_Author", (string)null);
                });

            modelBuilder.Entity("StoryBooks.Features.Authors.Domain.Entities.Battle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("DiceBattle")
                        .HasColumnType("bit");

                    b.Property<string>("EnemyName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Authors_Battle", (string)null);
                });

            modelBuilder.Entity("StoryBooks.Features.Authors.Domain.Entities.Book", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AuthorId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CoverId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("CoverId");

                    b.ToTable("Authors_Book", (string)null);
                });

            modelBuilder.Entity("StoryBooks.Features.Authors.Domain.Entities.BookCover", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<byte[]>("Content")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.ToTable("Authors_BookCover", (string)null);
                });

            modelBuilder.Entity("StoryBooks.Features.Authors.Domain.Entities.Chapter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("BattleId")
                        .HasColumnType("int");

                    b.Property<string>("BookId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("ChoiceId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsCheckpoint")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDiceChoice")
                        .HasColumnType("bit");

                    b.Property<bool>("IsFirstChapter")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.HasIndex("BattleId");

                    b.HasIndex("BookId");

                    b.HasIndex("ChoiceId");

                    b.ToTable("Authors_Chapter", (string)null);
                });

            modelBuilder.Entity("StoryBooks.Features.Authors.Domain.Entities.Choice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("NextChapterId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.HasIndex("NextChapterId");

                    b.ToTable("Authors_Choice", (string)null);
                });

            modelBuilder.Entity("StoryBooks.Features.Authors.Domain.Entities.Effect", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ChapterId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.HasIndex("ChapterId");

                    b.ToTable("Authors_Effect", (string)null);
                });

            modelBuilder.Entity("StoryBooks.Features.Authors.Domain.Entities.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Authors_Genre", (string)null);
                });

            modelBuilder.Entity("StoryBooks.Features.Authors.Domain.Entities.Stat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("BookId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsCritical")
                        .HasColumnType("bit");

                    b.Property<decimal>("MinimumValue")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("Value")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.ToTable("Authors_Stat", (string)null);
                });

            modelBuilder.Entity("StoryBooks.Features.Authors.Domain.Entities.StatModifier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("Change")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("EffectId")
                        .HasColumnType("int");

                    b.Property<int>("StatId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EffectId");

                    b.HasIndex("StatId");

                    b.ToTable("Authors_StatModifier", (string)null);
                });

            modelBuilder.Entity("StoryBooks.Features.Authors.Domain.Entities.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Authors_Tag", (string)null);
                });

            modelBuilder.Entity("BattleStat", b =>
                {
                    b.HasOne("StoryBooks.Features.Authors.Domain.Entities.Battle", null)
                        .WithMany()
                        .HasForeignKey("BattlesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StoryBooks.Features.Authors.Domain.Entities.Stat", null)
                        .WithMany()
                        .HasForeignKey("IncludedStatsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BookGenre", b =>
                {
                    b.HasOne("StoryBooks.Features.Authors.Domain.Entities.Book", null)
                        .WithMany()
                        .HasForeignKey("BooksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StoryBooks.Features.Authors.Domain.Entities.Genre", null)
                        .WithMany()
                        .HasForeignKey("GenresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BookTag", b =>
                {
                    b.HasOne("StoryBooks.Features.Authors.Domain.Entities.Book", null)
                        .WithMany()
                        .HasForeignKey("BooksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StoryBooks.Features.Authors.Domain.Entities.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StoryBooks.Features.Authors.Domain.Entities.Book", b =>
                {
                    b.HasOne("StoryBooks.Features.Authors.Domain.Entities.Author", "Author")
                        .WithMany("Books")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("StoryBooks.Features.Authors.Domain.Entities.BookCover", "Cover")
                        .WithMany()
                        .HasForeignKey("CoverId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.OwnsOne("StoryBooks.Features.Authors.Domain.Entities.BookStatus", "Status", b1 =>
                        {
                            b1.Property<string>("BookId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<int>("Value")
                                .HasColumnType("int");

                            b1.HasKey("BookId");

                            b1.ToTable("Authors_Book");

                            b1.WithOwner()
                                .HasForeignKey("BookId");
                        });

                    b.Navigation("Author");

                    b.Navigation("Cover");

                    b.Navigation("Status")
                        .IsRequired();
                });

            modelBuilder.Entity("StoryBooks.Features.Authors.Domain.Entities.Chapter", b =>
                {
                    b.HasOne("StoryBooks.Features.Authors.Domain.Entities.Battle", "Battle")
                        .WithMany()
                        .HasForeignKey("BattleId");

                    b.HasOne("StoryBooks.Features.Authors.Domain.Entities.Book", "Book")
                        .WithMany("Chapters")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StoryBooks.Features.Authors.Domain.Entities.Choice", "Choice")
                        .WithMany()
                        .HasForeignKey("ChoiceId");

                    b.Navigation("Battle");

                    b.Navigation("Book");

                    b.Navigation("Choice");
                });

            modelBuilder.Entity("StoryBooks.Features.Authors.Domain.Entities.Choice", b =>
                {
                    b.HasOne("StoryBooks.Features.Authors.Domain.Entities.Chapter", "NextChapter")
                        .WithMany("Choices")
                        .HasForeignKey("NextChapterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NextChapter");
                });

            modelBuilder.Entity("StoryBooks.Features.Authors.Domain.Entities.Effect", b =>
                {
                    b.HasOne("StoryBooks.Features.Authors.Domain.Entities.Chapter", "Chapter")
                        .WithMany("Effects")
                        .HasForeignKey("ChapterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chapter");
                });

            modelBuilder.Entity("StoryBooks.Features.Authors.Domain.Entities.Stat", b =>
                {
                    b.HasOne("StoryBooks.Features.Authors.Domain.Entities.Book", "Book")
                        .WithMany("Stats")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");
                });

            modelBuilder.Entity("StoryBooks.Features.Authors.Domain.Entities.StatModifier", b =>
                {
                    b.HasOne("StoryBooks.Features.Authors.Domain.Entities.Effect", null)
                        .WithMany("StatModifiers")
                        .HasForeignKey("EffectId");

                    b.HasOne("StoryBooks.Features.Authors.Domain.Entities.Stat", "Stat")
                        .WithMany()
                        .HasForeignKey("StatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Stat");
                });

            modelBuilder.Entity("StoryBooks.Features.Authors.Domain.Entities.Author", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("StoryBooks.Features.Authors.Domain.Entities.Book", b =>
                {
                    b.Navigation("Chapters");

                    b.Navigation("Stats");
                });

            modelBuilder.Entity("StoryBooks.Features.Authors.Domain.Entities.Chapter", b =>
                {
                    b.Navigation("Choices");

                    b.Navigation("Effects");
                });

            modelBuilder.Entity("StoryBooks.Features.Authors.Domain.Entities.Effect", b =>
                {
                    b.Navigation("StatModifiers");
                });
#pragma warning restore 612, 618
        }
    }
}
