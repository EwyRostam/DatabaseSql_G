using Databas_Examination_G.Entities;
using Databas_Examination_G.Repositories;
using Databas_Examination_G.Services;
using System.Diagnostics;


namespace Databas_Examination_G.Menus
{
    internal class MoviesMenu
    {
        private readonly MovieRepository _movieRepo;
        private readonly RatingService _ratingService;
        private readonly GenreService _genreService;
        private readonly ProducerService _producerService;
        private readonly DirectorService _directorService;

        public MoviesMenu(MovieRepository movieRepo, RatingService ratingService, GenreService genreService, ProducerService producerService, DirectorService directorService)
        {
            _movieRepo = movieRepo;
            _ratingService = ratingService;
            _genreService = genreService;
            _producerService = producerService;
            _directorService = directorService;
        }

        internal async Task MainMenuAsync()
        {

            var exit = false;
            do
            {
                Console.Clear();
                Console.WriteLine("1. Create new movie");
                Console.WriteLine("2. Show all movies");
                Console.WriteLine("3. Show details for specific movie");
                Console.WriteLine("4. Update a movie");
                Console.WriteLine("5. Delete a movie");
                Console.WriteLine("0. Go back to main menu");
                Console.Write("Choose one of the above alternatives (0-5): ");
                var option = Console.ReadLine();


                switch (option)
                {
                    case "1":
                        await CreateMenu();
                        break;

                    case "2":
                        await ListAllMenu();
                        break;

                    case "3":
                        await ListSpecificMenu();
                        break;

                    case "4":
                        await UpdateMenu();
                        break;

                    case "5":
                        await DeleteMenu();
                        break;

                    case "0":
                        exit = true;
                        break;

                    default:
                        break;
                }

            } while (exit == false);


        }

        private async Task CreateMenu()
        {
            Console.Clear(); //Clears field from main menu
            Console.WriteLine("Add a new movie");
            Console.WriteLine("-----------------------");

            var movie = new MovieEntity(); //Instance of new movie
            


            Console.Write("Name of movie: ");
            string movieName = Console.ReadLine()!.Trim();
            movie.Name = movieName;

            Console.Write("Description: ");
            string description = Console.ReadLine()!.Trim();
            movie.Description = description;

            Console.Write("Rate the movie from 1-5: ");
            var rating = int.Parse(Console.ReadLine()!.Trim());

            Console.Write("Year of release [xxxx]: ");
            var year = int.Parse(Console.ReadLine()!.Trim());
            movie.Year = year;

            Console.Write("Genre: ");
            var genre = Console.ReadLine()!.Trim();
            

            Console.Write("Production company: ");
            var producer = Console.ReadLine()!.Trim();
           

            Console.Write("First name of director: ");
            string firstName = Console.ReadLine()!.Trim().ToLower();
            if (firstName.Length > 0)
                firstName = char.ToUpper(firstName[0]) + firstName.Substring(1);

            Console.Write("Last name of director: ");
            string lastName = Console.ReadLine()!.Trim().ToLower();
            if (lastName.Length > 0)
                lastName = char.ToUpper(lastName[0]) + lastName.Substring(1);



            var newGenre = new GenreEntity()
            { Name = genre };
            newGenre = await _genreService.CreateGenreAsync(newGenre);

            var newRating = new RatingEntity()
            { Rating = rating };
            newRating = await _ratingService.CreateRatingAsync(newRating);

            var newProducer = new ProductionCompanyEntity()
            { Name = producer };
            newProducer = await _producerService.CreateProducerAsync(newProducer);

            var newDirector = new DirectorEntity()
            { FirstName = firstName, LastName = lastName };
            newDirector = await _directorService.CreateDirectorAsync(newDirector);

            var existMovie = await _movieRepo.ExistsAsync(x => x.Name == movieName);

            if (existMovie != true)
            {

                movie.Genre = newGenre;
                movie.Producer = newProducer;
                movie.Director = newDirector;
                movie.Rating = newRating;

                await _movieRepo.CreateAsync(movie);
                Console.Clear();
                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine("The movie has been added!");
                Console.ReadKey();

                await MainMenuAsync();
            }
            else
            {
                Console.WriteLine("The movie already exists!");
                Console.ReadKey();
                await MainMenuAsync();
            }
            
                      
        }

        private async Task ListAllMenu()
        {
            Console.Clear();
            Console.WriteLine("Show all movies");
            Console.WriteLine("---------------------");

            var list = await _movieRepo.GetAllAsync();

            if (list != null && list.Any())
            {
                foreach (var movie in list) //Loop for all movies in list
                {
                    Console.WriteLine($"{movie.Name}");
                    Console.WriteLine();
                }
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("There are no movies to show.");
                Thread.Sleep(2000); //Leaves the message for 2 seconds
                Console.Clear();
                for (var i = 0; i < 20; i++) //Prints 20 stars one after the other
                {
                    Console.Write("*");
                    Thread.Sleep(125);
                }
                Thread.Sleep(250);
                Console.Clear();
                await MainMenuAsync();
            }

        }

        private async Task<MovieEntity> ListSpecificMenu()
        {
            Console.Clear();
            Console.WriteLine("Search for the movie");
            Console.WriteLine("---------------------");
            Console.Write("Name: ");
            var name = Console.ReadLine()!.Trim();
            


            var movie = await _movieRepo.GetSpecificAsync(movie => movie.Name == name); //Compares the email with the email with the movies in the list and returns the first one matching.


            if (movie != null)
            {
                Console.WriteLine();
                Console.WriteLine($"{movie.Name} - {movie.Year} -- {movie.Genre.Name}");
                Console.WriteLine("***********************************");
                Console.WriteLine($"{movie.Description}");
                Console.WriteLine();
                Console.WriteLine($"Director: {movie.Director.Fullname}");
                Console.WriteLine();
                Console.WriteLine($"Production company: {movie.Producer.Name}");
                return movie;
            }

            else
            {
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine($"Couldn't find any movie with the name: \"{name}\"");
                Console.ReadKey();
                return null!;
            }
        }

        private async Task UpdateMenu()
        {
            try
            {
                var exit = false;

                MovieEntity movie = await ListSpecificMenu(); //Gets a movie from list through "ListSpecificMenu"

                if (movie != null)
                {
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("1. Update name");
                        Console.WriteLine("2. Update description");
                        Console.WriteLine("3. Update genre");
                        Console.WriteLine("4. Update year");
                        Console.WriteLine("5. Update production company");
                        Console.WriteLine("6. Update director");
                        Console.WriteLine("7. Update rating");
                        Console.WriteLine("0. Go back");
                        Console.Write("Choose one of the above options (0-5): ");
                        var option = Console.ReadLine();
                        Console.Clear();


                        switch (option)
                        {
                            case "1":
                                Console.Write("Enter new movie name: ");
                                string name = Console.ReadLine()!.Trim();
                                movie.Name = name;

                                Console.WriteLine("Name of movie has now been updated!");
                                break;

                            case "2":
                                Console.Write("Enter new description: ");
                                string description = Console.ReadLine()!.Trim();
                                movie.Description = description;
                                Console.Clear();
                                Console.WriteLine("Description has now been updated!");
                                break;

                            case "3":
                                Console.Write("Enter new genre: ");
                                 string genre = Console.ReadLine()!.Trim().ToLower();
                                if (genre.Length > 0)
                                    movie.Genre.Name = char.ToUpper(genre[0]) + genre.Substring(1);
                                Console.Clear();

                                Console.WriteLine("The genre has now been updated!");
                                break;

                            case "4":
                                Console.Write("Enter new release year [xxxx]: ");
                                movie.Year = int.Parse(Console.ReadLine()!.Trim());
                                Console.Clear();
                                Console.WriteLine("The death year has now been updated!");
                                break;

                            case "5":
                                Console.Clear();
                                Console.Write("Enter new production company: ");
                                movie.Producer.Name = Console.ReadLine()!.Trim();
                                break;

                            case "6":
                                Console.Write("Enter new first name: ");
                                string firstName = Console.ReadLine()!.Trim().ToLower();
                                if (firstName.Length > 0)
                                    movie.Director.FirstName = char.ToUpper(firstName[0]) + firstName.Substring(1);

                                Console.WriteLine("First name has now been updated!");
                                

                                Console.Write("Enter new last name: ");
                                string lastName = Console.ReadLine()!.Trim().ToLower();
                                if (lastName.Length > 0)
                                    movie.Director.LastName = char.ToUpper(lastName[0]) + lastName.Substring(1);
                                Console.Clear();
                                Console.WriteLine("Lastname has now been updated!");
                                break;

                            case "7":
                                Console.Write("Enter new rating from 1-5: ");
                                movie.Rating!.Rating = int.Parse(Console.ReadLine()!.Trim());
                                Console.Clear();
                                Console.WriteLine("Rating has been updated!");
                                break;

                            case "0":
                                exit = true;
                                break;

                            default:
                                break;
                        }

                    } while (exit == false);

                    await _movieRepo.UpdateAsync(movie);

                }

            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
        }

        private async Task DeleteMenu()
        {
            Console.Clear();
            Console.WriteLine("Search for the movie you want to delete");
            Console.WriteLine("------------------------------------");
            Console.Clear();
            Console.WriteLine("Search for the movie");
            Console.WriteLine("---------------------");
            Console.Write("Name: ");
            var name = Console.ReadLine()!.Trim();



            var movie = await _movieRepo.GetSpecificAsync(movie => movie.Name == name); //Compares the email with the email with the movies in the list and returns the first one matching.

            if (movie != null)
            {
                Console.WriteLine();
                Console.WriteLine($"{movie.Name} - {movie.Year} - {movie.Genre.Name}");
                Console.WriteLine("***********************************");
                Console.WriteLine($"{movie.Description}");
                Console.WriteLine();
                Console.WriteLine($"Director: {movie.Director.Fullname}");
                Console.WriteLine();
                Console.WriteLine($"Production company: {movie.Producer.Name}");
                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine("Press any key to delete movie");
                Console.ReadKey();
                await _movieRepo.DeleteAsync(x => x.Name == name);
                Console.Clear();


                for (var i = 0; i < 20; i++)
                {
                    Console.Write("*");
                    Thread.Sleep(125);
                }
                Thread.Sleep(250);
                Console.Clear();
                Console.WriteLine($"The movie \"{movie.Name}\"  has now been deleted.");
                Thread.Sleep(2000);
                Console.Clear();
            }


            else
            {
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine($"Couldn't find any movie with the name: \"{name}\"");
                Console.ReadKey();
            }
        }
    }
}