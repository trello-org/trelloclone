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
	public class CardLabelConfiguration : IEntityTypeConfiguration<Label>
	{
		public void Configure(EntityTypeBuilder<Label> builder)
		{
			builder.ToTable("labels");
			builder.HasKey(k => k.Id);
			builder.Property(en => en.Id).HasColumnName("id").IsRequired();
			builder.Property(en => en.Name).HasColumnName("name").IsRequired();
			builder.Property(en => en.ColorHex).HasColumnName("color_hex").IsRequired();
			builder.Property(en => en.CardId).HasColumnName("card_id").IsRequired();
			
		}
	}
}
