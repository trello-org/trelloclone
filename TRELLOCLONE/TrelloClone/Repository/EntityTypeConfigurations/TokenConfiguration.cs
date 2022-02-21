using Application.Dtos;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.EntityTypeConfigurations
{
	public class TokenConfiguration : IEntityTypeConfiguration<RefreshToken>
	{

		public void Configure(EntityTypeBuilder<RefreshToken> builder)
		{
			builder.ToTable("refresh_tokens");
			builder.HasIndex(t => t.Token).IsUnique();
			builder.HasKey(t => t.Id);
			builder.Property(en => en.Id).HasColumnName("id").IsRequired();
			builder.Property(en => en.Token).HasColumnName("token").IsRequired();
			builder.Property(en => en.Expires).HasColumnName("expires").IsRequired();
			builder.Property(en => en.Created).HasColumnName("created").IsRequired();
			builder.Property(en => en.CreatedByIp).HasColumnName("created_ip").IsRequired();
			builder.Property(en => en.Revoked).HasColumnName("revoked");
			builder.Property(en => en.RevokedByIp).HasColumnName("revoked_ip");
			builder.Property(en => en.ReplacedByToken).HasColumnName("replaced_by");
			builder.Property(en => en.UserId).HasColumnName("user_id").IsRequired();
		}
	}
}
