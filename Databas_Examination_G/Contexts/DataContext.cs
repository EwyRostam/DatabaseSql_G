using Databas_Examination_G.Entities;
using Microsoft.EntityFrameworkCore;

namespace Databas_Examination_G.Contexts;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    public DbSet<DirectorEntity> Directors { get; set; }
    public DbSet<GenreEntity> Genres { get; set; }
    public  DbSet<MovieEntity> Movies { get; set; }
    public DbSet<RatingEntity> Ratings { get; set; }
    public DbSet<ProductionCompanyEntity> ProductionCompanies { get; set; }


}
