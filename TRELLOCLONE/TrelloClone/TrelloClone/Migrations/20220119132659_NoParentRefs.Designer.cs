﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TrelloClone;

namespace TrelloClone.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20220119132659_NoParentRefs")]
    partial class NoParentRefs
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

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Boards");
                });

            modelBuilder.Entity("TrelloClone.Models.Card", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CardListId")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CardListId");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("TrelloClone.Models.CardList", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("BoardId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("BoardId");

                    b.ToTable("CardLists");
                });

            modelBuilder.Entity("TrelloClone.Models.Label", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CardId")
                        .HasColumnType("uuid");

                    b.Property<string>("ColorHex")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CardId");

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
                    b.HasOne("TrelloClone.Models.User", null)
                        .WithMany("Boards")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("TrelloClone.Models.Card", b =>
                {
                    b.HasOne("TrelloClone.Models.CardList", null)
                        .WithMany("Cards")
                        .HasForeignKey("CardListId");
                });

            modelBuilder.Entity("TrelloClone.Models.CardList", b =>
                {
                    b.HasOne("TrelloClone.Models.Board", null)
                        .WithMany("CardLists")
                        .HasForeignKey("BoardId");
                });

            modelBuilder.Entity("TrelloClone.Models.Label", b =>
                {
                    b.HasOne("TrelloClone.Models.Card", null)
                        .WithMany("Labels")
                        .HasForeignKey("CardId");
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
