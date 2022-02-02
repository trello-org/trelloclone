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
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

		}
	}
}
