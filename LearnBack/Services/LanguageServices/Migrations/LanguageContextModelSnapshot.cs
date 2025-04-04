﻿// <auto-generated />
using LanguageServices.SQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LanguageServices.Migrations
{
    [DbContext(typeof(LanguageContext))]
    partial class LanguageContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.2");

            modelBuilder.Entity("LanguageLib.Article", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("LanguageLib.Paragraph", b =>
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

            modelBuilder.Entity("LanguageLib.Sentence", b =>
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

            modelBuilder.Entity("LanguageLib.Word", b =>
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

            modelBuilder.Entity("LanguageLib.Paragraph", b =>
                {
                    b.HasOne("LanguageLib.Article", null)
                        .WithMany("Paragraphs")
                        .HasForeignKey("ArticleId");
                });

            modelBuilder.Entity("LanguageLib.Sentence", b =>
                {
                    b.HasOne("LanguageLib.Paragraph", null)
                        .WithMany("Sentences")
                        .HasForeignKey("ParagraphId");
                });

            modelBuilder.Entity("LanguageLib.Word", b =>
                {
                    b.HasOne("LanguageLib.Sentence", null)
                        .WithMany("Words")
                        .HasForeignKey("SentenceId");
                });

            modelBuilder.Entity("LanguageLib.Article", b =>
                {
                    b.Navigation("Paragraphs");
                });

            modelBuilder.Entity("LanguageLib.Paragraph", b =>
                {
                    b.Navigation("Sentences");
                });

            modelBuilder.Entity("LanguageLib.Sentence", b =>
                {
                    b.Navigation("Words");
                });
#pragma warning restore 612, 618
        }
    }
}
