using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrelloClone.Models;

namespace Repository.EntityTypeConfigurations
{
	public class BoardConfiguration : IEntityTypeConfiguration<Board>
	{
		public void Configure(EntityTypeBuilder<Board> builder)
		{
			builder.ToTable("boards");
			builder.HasIndex(b => b.Name).IsUnique();
			builder.HasKey(k => k.Id);
			builder.Property(en => en.Id).HasColumnName("id").IsRequired();
			builder.Property(en => en.Name).HasColumnName("name").IsRequired();
			builder.Property(en => en.Description).HasColumnName("description");
			builder.Property(en => en.BackgroundUrl).HasColumnName("background_url");
			builder.Property(en => en.IsPublic).HasColumnName("is_public").IsRequired();
			builder.Property(en => en.UserId).HasColumnName("user_id").IsRequired();

		}
	}
}
