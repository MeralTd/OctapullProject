using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Persistence.Context;

public partial class DataContext : DbContext
{
    protected IConfiguration Configuration { get; set; }

    public virtual DbSet<Meeting> Meetings { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<MeetingDeleteLog> MeetingDeleteLog { get; set; }

    public DataContext()
    {

    }

    public DataContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
    {
        Configuration = configuration;
    }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlServer("Server=.; Database=OctapullDB;User Id=sa;Password=1; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

}
