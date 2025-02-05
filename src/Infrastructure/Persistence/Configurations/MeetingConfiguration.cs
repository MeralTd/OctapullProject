using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class MeetingConfiguration : BaseConfiguration<Meeting>
{
    public override void Configure(EntityTypeBuilder<Meeting> builder)
    {
        builder.ToTable("Meetings");
        builder.Property(e => e.CreateDate).HasColumnType("datetime");
        builder.Property(e => e.Description).HasMaxLength(500);
        builder.Property(e => e.EndDate).HasColumnType("datetime");
        builder.Property(e => e.MeetingName).HasMaxLength(50);
        builder.Property(e => e.StartDate).HasColumnType("datetime");
        builder.Property(e => e.UpdateDate).HasColumnType("datetime");

        base.Configure(builder);
    }
}
