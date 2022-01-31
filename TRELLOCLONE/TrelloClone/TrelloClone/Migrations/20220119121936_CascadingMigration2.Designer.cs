﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Repository;

namespace TrelloClone.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20220119121936_CascadingMigration2")]
    partial class CascadingMigration2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("TrelloClone.Models.Board", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("BackgroundUrl")
                        .HasColumnType("text");

                    b.Property<Guid?>("BoardOwnerId")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("BoardOwnerId");

                    b.ToTable("Boards");
                });

            modelBuilder.Entity("TrelloClone.Models.Card", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("BelongsToCardListId")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("BelongsToCardListId");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("TrelloClone.Models.CardList", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("BelongsToBoardId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("BelongsToBoardId");

                    b.ToTable("CardLists");
                });

            modelBuilder.Entity("TrelloClone.Models.Label", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("BelongsToCardId")
                        .HasColumnType("uuid");

                    b.Property<string>("ColorHex")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("BelongsToCardId");

                    b.ToTable("Labels");
                });

            modelBuilder.Entity("TrelloClone.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TrelloClone.Models.Board", b =>
                {
                    b.HasOne("TrelloClone.Models.User", "BoardOwner")
                        .WithMany("Boards")
                        .HasForeignKey("BoardOwnerId")
                        .OnDelete(DeleteBehavior.ClientCascade);

                    b.Navigation("BoardOwner");
                });

            modelBuilder.Entity("TrelloClone.Models.Card", b =>
                {
                    b.HasOne("TrelloClone.Models.CardList", "BelongsToCardList")
                        .WithMany("Cards")
                        .HasForeignKey("BelongsToCardListId")
                        .OnDelete(DeleteBehavior.ClientCascade);

                    b.Navigation("BelongsToCardList");
                });

            modelBuilder.Entity("TrelloClone.Models.CardList", b =>
                {
                    b.HasOne("TrelloClone.Models.Board", "BelongsToBoard")
                        .WithMany("CardLists")
                        .HasForeignKey("BelongsToBoardId")
                        .OnDelete(DeleteBehavior.ClientCascade);

                    b.Navigation("BelongsToBoard");
                });

            modelBuilder.Entity("TrelloClone.Models.Label", b =>
                {
                    b.HasOne("TrelloClone.Models.Card", "BelongsToCard")
                        .WithMany("Labels")
                        .HasForeignKey("BelongsToCardId")
                        .OnDelete(DeleteBehavior.ClientCascade);

                    b.Navigation("BelongsToCard");
                });

            modelBuilder.Entity("TrelloClone.Models.Board", b =>
                {
                    b.Navigation("CardLists");
                });

            modelBuilder.Entity("TrelloClone.Models.Card", b =>
                {
                    b.Navigation("Labels");
                });

            modelBuilder.Entity("TrelloClone.Models.CardList", b =>
                {
                    b.Navigation("Cards");
                });

            modelBuilder.Entity("TrelloClone.Models.User", b =>
                {
                    b.Navigation("Boards");
                });
#pragma warning restore 612, 618
        }
    }
}
