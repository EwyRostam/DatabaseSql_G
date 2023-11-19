using Databas_Examination_G.Entities;
using Databas_Examination_G.Models;
using Databas_Examination_G.Repositories;

namespace Databas_Examination_G.Services;

internal class MovieService
{
    private readonly MovieRepository _movieRepo;
    private readonly DirectorService _directorService;
    private readonly GenreService _genreService;
    private readonly ProducerService _producerService;
    private readonly RatingService _ratingService;

    public MovieService(MovieRepository movieRepo, DirectorService directorService, GenreService genreService, ProducerService producerService, RatingService ratingService)
    {
        _movieRepo = movieRepo;
        _directorService = directorService;
        _genreService = genreService;
        _producerService = producerService;
        _ratingService = ratingService;
    }

    public async Task<bool> CreateMovieAsync(MovieRegistration movie)
    {

     
        // check customer
        if (!await _movieRepo.ExistsAsync(x => x.Name == movie.Name))
        {
            // check address
            DirectorEntity director = await _directorService.GetSpecificAsync(movie.DirectorFirstName, movie.DirectorLastName);
            if(director == null)
                director = await _directorService.CreateDirectorAsync(new DirectorEntity { FirstName = movie.DirectorFirstName, LastName = movie.DirectorLastName });
            

            GenreEntity genre = await _genreService.GetSpecificAsync(movie.GenreName);
            if (genre == null)
                genre = await _genreService.CreateGenreAsync(new GenreEntity { Name = movie.GenreName});

            RatingEntity rating = await _ratingService.GetSpecificAsync(movie.Rating);
            if (rating == null)
                rating = await _ratingService.CreateRatingAsync(new RatingEntity { Rating = movie.Rating});


            ProductionCompanyEntity producer = await _producerService.GetSpecificAsync(movie.ProducerName);
            if (producer == null)
                producer = await _producerService.CreateProducerAsync(new ProductionCompanyEntity { Name = movie.ProducerName });

            // create customer
            MovieEntity movieEntity = await _movieRepo.CreateAsync(new MovieEntity { Name = movie.Name, Year = movie.Year, Description = movie.Description, DirectorId = director.Id, GenreId = genre.Id, RatingId = rating.Id, ProducerId = producer.Id });
            if (movieEntity != null)
                return true;

        }

        return false;

    }

    public async Task<IEnumerable<MovieEntity>> GetAllAsync()
    {
        try
        {
            var list = await _movieRepo.GetAllAsync();
            return list;
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }
        return null!;

    }

    public async Task<MovieEntity> GetSpecificAsync(string name)
    {
        try
        {
            var movie = await _movieRepo.GetSpecificAsync(x => x.Name == name);
            return movie ?? null!;
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }
        return null!;
    }

    public async Task<MovieEntity> UpdateAsync(MovieEntity entity)
    {
        try
        {
            var movie = await _movieRepo.UpdateAsync(entity);
            return movie;

        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }
        return null!;
    }

    public async Task<bool> DeleteAsync(string name)
    {
        var result = await _movieRepo.DeleteAsync(x => x.Name == name);
        return result;
    }
}
