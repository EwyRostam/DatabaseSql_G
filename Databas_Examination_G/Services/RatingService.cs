
using Databas_Examination_G.Entities;
using Databas_Examination_G.Repositories;
using System.Diagnostics;

namespace Databas_Examination_G.Services;

internal class RatingService
{
    private readonly RatingRepository _repo;

    public RatingService(RatingRepository repo)
    {
        _repo = repo;
    }

    public async Task<RatingEntity> CreateRatingAsync(RatingEntity entity)
    {
        try
        {
            var result = await _repo.ExistsAsync(x => x.Rating == entity.Rating);
            if (!result)
            {
                var ratingEntity = await _repo.CreateAsync(entity);
                return ratingEntity;
            }
            else
            return entity;

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;

    }
    public async Task<IEnumerable<RatingEntity>> GetAllAsync()
    {
        try
        {
            var list = await _repo.GetAllAsync();
            return list;
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }
        return null!;

    }

    public async Task<RatingEntity> GetSpecificAsync(int rating)
    {
        try
        {
            var producer = await _repo.GetSpecificAsync(x => x.Rating == rating);
            return producer ?? null!;
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }
        return null!;
    }

    public async Task<RatingEntity> UpdateAsync(RatingEntity entity)
    {
        try
        {
            var producer = await _repo.UpdateAsync(entity);
            return producer;

        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }
        return null!;
    }

    public async Task<bool> DeleteAsync(int rating)
    {
        var result = await _repo.DeleteAsync(x => x.Rating == rating);
        return result;
    }
}
