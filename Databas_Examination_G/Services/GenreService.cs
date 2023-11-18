using Databas_Examination_G.Entities;
using Databas_Examination_G.Repositories;
using System.Diagnostics;

namespace Databas_Examination_G.Services;

internal class GenreService
{
    private readonly GenreRepository _repo;

    public GenreService(GenreRepository repo)
    {
        _repo = repo;
    }

    public async Task<GenreEntity> CreateGenreAsync(GenreEntity entity)
    {
        try
        {
            var result = await _repo.ExistsAsync(x => x.Name == entity.Name);
            if (!result)
            {
                var genreEntity = await _repo.CreateAsync(entity);
                return genreEntity;
            }
            else
            return null!;

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;

    }

    public async Task<IEnumerable<GenreEntity>> GetAllAsync()
    {
        try
        {
            var list = await _repo.GetAllAsync();
            return list;
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }
        return null!;

    }

    public async Task<GenreEntity> GetSpecificAsync(string name)
    {
        try
        {
            var genre = await _repo.GetSpecificAsync(x => x.Name == name);
            return genre ?? null!;
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }
        return null!;
    }

    public async Task<GenreEntity> UpdateAsync(GenreEntity entity)
    {
        try
        {
            var Genre = await _repo.UpdateAsync(entity);
            return Genre;

        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }
        return null!;
    }

    public async Task<bool> DeleteAsync(string name)
    {
        var result = await _repo.DeleteAsync(x => x.Name == name);
        return result;
    }
}
