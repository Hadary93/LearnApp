﻿// <auto-generated />
using LanguageServices.SQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LanguageServices.Migrations
{
    [DbContext(typeof(LanguageContext))]
    [Migration("20250313011723_updateHash")]
    partial class updateHash
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.2");

            modelBuilder.Entity("Language.Article", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("Language.Paragraph", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ArticleId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.ToTable("Paragraphs");
                });

            modelBuilder.Entity("Language.Sentence", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Difficulty")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Hash")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("ParagraphId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SentenceCount")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ParagraphId");

                    b.ToTable("Sentences");
                });

            modelBuilder.Entity("Learn.Word", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Difficulty")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Favourite")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Group")
                        .HasColumnType("TEXT");

                    b.Property<int?>("SentenceId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Translation")
                        .HasColumnType("TEXT");

                    b.Property<int>("WordCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("WordText")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("SentenceId");

                    b.ToTable("Words");
                });

            modelBuilder.Entity("Language.Paragraph", b =>
                {
                    b.HasOne("Language.Article", null)
                        .WithMany("Paragraphs")
                        .HasForeignKey("ArticleId");
                });

            modelBuilder.Entity("Language.Sentence", b =>
                {
                    b.HasOne("Language.Paragraph", null)
                        .WithMany("Sentences")
                        .HasForeignKey("ParagraphId");
                });

            modelBuilder.Entity("Learn.Word", b =>
                {
                    b.HasOne("Language.Sentence", null)
                        .WithMany("Words")
                        .HasForeignKey("SentenceId");
                });

            modelBuilder.Entity("Language.Article", b =>
                {
                    b.Navigation("Paragraphs");
                });

            modelBuilder.Entity("Language.Paragraph", b =>
                {
                    b.Navigation("Sentences");
                });

            modelBuilder.Entity("Language.Sentence", b =>
                {
                    b.Navigation("Words");
                });
#pragma warning restore 612, 618
        }
    }
}
