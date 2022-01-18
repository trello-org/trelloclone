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
	}
}
