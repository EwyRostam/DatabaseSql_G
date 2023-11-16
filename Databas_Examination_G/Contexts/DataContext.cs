using Databas_Examination_G.Entities;
using Microsoft.EntityFrameworkCore;

namespace Databas_Examination_G.Contexts;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    public DbSet<DirectorEntity> Entities { get; set; }
    public DbSet<GenreEntity> Genre { get; set; }
   public  DbSet<MovieEntity> Movies { get; set; }
    public DbSet<MovieGenreEntity> MovieGenres { get; set; }
    public DbSet<ProductionCompanyEntity> ProductionCompany { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MovieGenreEntity>().HasKey(x => new { x.MovieId, x.GenreId });
    }
}
