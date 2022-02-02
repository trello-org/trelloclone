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
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.ToTable("users");
			builder.HasIndex(u => u.Username).IsUnique();
			builder.HasKey(k => k.Id);
			builder.Property(en => en.Id).HasColumnName("id").IsRequired(); ;
			builder.Property(en => en.Username).HasColumnName("username").IsRequired();
			builder.Property(en => en.Password).HasColumnName("password").IsRequired();
		}
	}
}
