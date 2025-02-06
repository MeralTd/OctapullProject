using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class UserConfiguration : BaseConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("EMail");
        builder.Property(e => e.FirstName).HasMaxLength(50);
        builder.Property(e => e.LastName).HasMaxLength(50);
        builder.Property(e => e.PhoneNumber).HasMaxLength(15);

        builder.HasMany(u => u.RefreshTokens);

        base.Configure(builder);
    }
}
