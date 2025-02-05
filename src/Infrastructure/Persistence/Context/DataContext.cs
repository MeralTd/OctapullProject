using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Persistence.Context;

public partial class DataContext : DbContext
{
    protected IConfiguration Configuration { get; set; }

    public virtual DbSet<Meeting> Meetings { get; set; }

    public virtual DbSet<User> Users { get; set; }


    public DataContext()
    {

    }

    public DataContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
    {
        Configuration = configuration;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        IEnumerable<EntityEntry<BaseEntity>> entries = ChangeTracker
            .Entries<BaseEntity>()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        foreach (EntityEntry<BaseEntity> entry in entries)
            _ = entry.State switch
            {
                EntityState.Added => entry.Entity.CreatedDate = DateTime.UtcNow,
                EntityState.Modified => entry.Entity.UpdatedDate = DateTime.UtcNow
            };
        return await base.SaveChangesAsync(cancellationToken);
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlServer("Server=.; Database=OctapullDB;User Id=sa;Password=1; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

}
