using Databas_Examination_G.Contexts;
using Databas_Examination_G.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Databas_Examination_G.Repositories;

internal class DirectorRepository : Repo<DirectorEntity>
{
    private readonly DataContext _context;
    public DirectorRepository(DataContext context) : base(context)
    {
        _context = context;
    }

}

internal class GenreRepository : Repo<GenreEntity>
{
    private readonly DataContext _context;
    public GenreRepository(DataContext context) : base(context)
    { 
        _context = context;
    }
}

internal class MovieRepository : Repo<MovieEntity>
{
    private DataContext _context;
    public MovieRepository(DataContext context) : base(context)
    {
        _context = context; 
    }

    public override async Task<IEnumerable<MovieEntity>> GetAllAsync()
    {
        return await _context.Movies
            .Include(x => x.Director)
            .Include(x => x.Genre)
            .Include(x => x.Producer)
            .ToListAsync();
    }

    public override async Task<MovieEntity> GetSpecificAsync(Expression<Func<MovieEntity, bool>> predicate)
    {
        var result = await _context.Movies
            .Include(x => x.Director)
            .Include(x => x.Genre)
            .Include(x => x.Producer).FirstOrDefaultAsync();

        if (result != null)
            return result;

        return null!;
    }
}

internal class RatingRepository : Repo<RatingEntity>
{
    public RatingRepository(DataContext context) : base(context)
    {
    }
}

internal class ProductionCompanyRepository : Repo<ProductionCompanyEntity>
{
    public ProductionCompanyRepository(DataContext context) : base(context)
    {
    }
}