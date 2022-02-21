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
	public class CardConfiguration : IEntityTypeConfiguration<Card>
	{
		public void Configure(EntityTypeBuilder<Card> builder)
		{
			builder.ToTable("cards");

			builder.HasKey(k => k.Id);
			builder.Property(en => en.Id).HasColumnName("id").IsRequired();
			builder.Property(en => en.Description).HasColumnName("description").IsRequired();
			builder.Property(en => en.CardListId).HasColumnName("card_list_id").IsRequired();
			
		}
	}
}
