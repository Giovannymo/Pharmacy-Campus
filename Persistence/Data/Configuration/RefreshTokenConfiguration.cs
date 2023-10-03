
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("RefreshToken");

        builder.HasOne(p => p.User)
        .WithMany(p => p.RefreshTokens)
        .HasForeignKey(p => p.User_Fk);

        builder.Property(p => p.Token)
        .IsRequired()
        .HasMaxLength(300);

        builder.Property(p => p.Created)
        .IsRequired()
        .HasColumnType("DateTime");

        builder.Property(p => p.Expires)
        .IsRequired()
        .HasColumnType("DateTime");

        builder.Property(p => p.Revoked)
        .HasColumnType("DateTime");

    }
}