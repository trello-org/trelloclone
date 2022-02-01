using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TrelloClone.Models;

namespace Repository
{
	public class ApplicationContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Board> Boards { get; set; }
		public DbSet<CardList> CardLists { get; set; }
		public DbSet<Card> Cards { get; set; }
		public DbSet<Label> Labels { get; set; }

		public ApplicationContext(DbContextOptions options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("TrelloClone.Repository"));

			// modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());

			modelBuilder.Entity<User>().ToTable("users");
			modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();
			modelBuilder.Entity<User>(u =>
			{
				u.HasKey(k => k.Id);
				u.Property(en => en.Id).HasColumnName("id").IsRequired(); ;
				u.Property(en => en.Username).HasColumnName("username").IsRequired();
				u.Property(en => en.Password).HasColumnName("password").IsRequired();

			});
				
			modelBuilder.Entity<Board>().ToTable("boards");
			modelBuilder.Entity<Board>().HasIndex(b => b.Name).IsUnique();
			modelBuilder.Entity<Board>(b =>
			{
				b.HasKey(k => k.Id);
				b.Property(en => en.Id).HasColumnName("id").IsRequired();
				b.Property(en => en.Name).HasColumnName("name").IsRequired();
				b.Property(en => en.Description).HasColumnName("description");
				b.Property(en => en.BackgroundUrl).HasColumnName("background_url");
				b.Property(en => en.IsPublic).HasColumnName("is_public").IsRequired();
				b.Property(en => en.UserId).HasColumnName("user_id").IsRequired();
			});

			modelBuilder.Entity<CardList>().ToTable("cardlists");
			modelBuilder.Entity<CardList>(cl => {
				cl.HasKey(k => k.Id);
				cl.Property(en => en.Id).HasColumnName("id").IsRequired();
				cl.Property(en => en.Name).HasColumnName("name").IsRequired();
				cl.Property(en => en.BoardId).HasColumnName("board_id").IsRequired();
			});
			modelBuilder.Entity<Card>().ToTable("cards");
			modelBuilder.Entity<Card>(c =>
			{
				c.HasKey(k => k.Id);
				c.Property(en => en.Id).HasColumnName("id").IsRequired();
				c.Property(en => en.Description).HasColumnName("description").IsRequired();
				c.Property(en => en.CardListId).HasColumnName("card_list_id").IsRequired();
			});

			modelBuilder.Entity<Label>().ToTable("labels");
			modelBuilder.Entity<Label>(l =>
			{
				l.HasKey(k => k.Id);
				l.Property(en => en.Id).HasColumnName("id").IsRequired();
				l.Property(en => en.Name).HasColumnName("name").IsRequired();
				l.Property(en => en.ColorHex).HasColumnName("color_hex").IsRequired();
				l.Property(en => en.CardId).HasColumnName("card_id").IsRequired();
			});

			//modelBuilder.Entity<Board>().Property(b => b.Name).IsRequired();
			//modelBuilder.Entity<CardList>().Property(cl => cl.Name).IsRequired();
			//modelBuilder.Entity<Card>().Property(c => c.Name).IsRequired();
			//modelBuilder.Entity<Label>().Property(l => l.Name).IsRequired();

		}
	}
}
