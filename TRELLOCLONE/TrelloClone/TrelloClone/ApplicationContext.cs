using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrelloClone.Models;

namespace TrelloClone
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
			modelBuilder.Entity<User>().Property(u => u.Username).IsRequired();
			modelBuilder.Entity<User>().Property(u => u.Password).IsRequired();
			modelBuilder.Entity<Board>().Property(b => b.Name).IsRequired();
			modelBuilder.Entity<CardList>().Property(cl => cl.Name).IsRequired();
			modelBuilder.Entity<Card>().Property(c => c.Name).IsRequired();
			modelBuilder.Entity<Label>().Property(l => l.Name).IsRequired();
		}
	}
}
