using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrelloClone.Models;

namespace Repository.Config
{
	public class CardListConfiguration : IEntityTypeConfiguration<CardList>
	{
		public void Configure(EntityTypeBuilder<CardList> builder)
		{
			builder.ToTable("cardlists");

			builder.HasKey(k => k.Id);
			builder.Property(en => en.Id).HasColumnName("id").IsRequired();
			builder.Property(en => en.Name).HasColumnName("name").IsRequired();
			builder.Property(en => en.BoardId).HasColumnName("board_id").IsRequired();
			
		}
	}
}
