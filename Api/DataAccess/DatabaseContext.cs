using Microsoft.EntityFrameworkCore;

namespace Api.DataAccess;

public class DatabaseContext(DbContextOptions options) : DbContext(options)
{
    // public DbSet<Model1> Model1 { get; set; }
    // public DbSet<Model2> Model2 { get; set; }
    // public DbSet<Model3> Model3 { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.HasPostgresEnum<Enum1>();
        // modelBuilder.HasPostgresEnum<Enum2>();
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
    }
}