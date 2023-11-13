using Databas_Examination_G.Entities;
using Microsoft.EntityFrameworkCore;

namespace Databas_Examination_G.Contexts;

internal class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    DbSet<DirectorEntity> Entities { get; set; }
    DbSet<GenreEntity> Genre { get; set; }
    DbSet<MovieEntity> Movies { get; set; }
    DbSet<MovieGenreEntity> MovieGenres { get; set; }
    DbSet<ProductionCompanyEntity> ProductionCompany { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
}
