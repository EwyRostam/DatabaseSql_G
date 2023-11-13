using Databas_Examination_G.Contexts;
using Databas_Examination_G.Entities;

namespace Databas_Examination_G.Repositories;

internal class DirectorRepository : Repo<DirectorEntity>
{
    public DirectorRepository(DataContext context) : base(context)
    {
    }
}

internal class GenreRepository : Repo<GenreEntity>
{
    public GenreRepository(DataContext context) : base(context)
    {
    }
}

internal class MovieRepository : Repo<MovieEntity>
{
    public MovieRepository(DataContext context) : base(context)
    {
    }
}

internal class MovieGenreRepository : Repo<MovieGenreEntity>
{
    public MovieGenreRepository(DataContext context) : base(context)
    {
    }
}

internal class ProductionCompanyRepository : Repo<ProductionCompanyEntity>
{
    public ProductionCompanyRepository(DataContext context) : base(context)
    {
    }
}