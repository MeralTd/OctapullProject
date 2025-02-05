using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class RefreshTokenConfiguration : BaseConfiguration<RefreshToken>
{
    public override void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("RefreshTokens");
        builder.Property(x => x.UserId).HasColumnName("UserID").IsRequired();
        builder.Property(x => x.Token).HasColumnName("Token").IsRequired();
        builder.Property(x => x.Expires).HasColumnName("Expires").IsRequired();
        builder.Property(x => x.CreatedByIp).HasColumnName("CreatedByIP").IsRequired();
        builder.Property(x => x.Revoked).HasColumnName("Revoked");
        builder.Property(x => x.RevokedByIp).HasColumnName("RevokedByIP");
        builder.Property(x => x.ReplacedByToken).HasColumnName("ReplacedByToken");
        builder.Property(x => x.ReasonRevoked).HasColumnName("ReasonRevoked");


        builder.HasOne(x => x.User);
        base.Configure(builder);
    }
}