using Databas_Examination_G.Entities;
using Databas_Examination_G.Repositories;
using System.Diagnostics;
using System.IO;

namespace Databas_Examination_G.Menus
{
    internal class MoviesMenu
    {
        private readonly MovieRepository _movieRepo;
        private readonly MovieGenreRepository _movieGenreRepo;
        private readonly GenreRepository _genreRepo;
        private readonly ProductionCompanyRepository _producerRepo;
        private readonly DirectorRepository _directorRepo;

        public MoviesMenu(MovieRepository movieRepo, MovieGenreRepository movieGenreRepo, GenreRepository genreRepo, ProductionCompanyRepository producerRepo, DirectorRepository directorRepo)
        {
            _movieRepo = movieRepo;
            _movieGenreRepo = movieGenreRepo;
            _genreRepo = genreRepo;
            _producerRepo = producerRepo;
            _directorRepo = directorRepo;
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
                Console.WriteLine("0. Close program");
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
            var movieGenre = new MovieGenreEntity();


            Console.Write("Name of movie: ");
            string movieName = Console.ReadLine()!.Trim();
            movie.Name = movieName;

            Console.Write("Description: ");
            string description = Console.ReadLine()!.Trim();
            movie.Description = description;

            Console.Write("Year of release [xxxx]: ");
            var year = int.Parse(Console.ReadLine()!.Trim());
            movie.Year = year;

            Console.Write("Genre: ");
            var genre = Console.ReadLine()!.Trim();
            movie.MovieGenre.Genre.Name = genre;

            Console.Write("Production company: ");
            var producer = Console.ReadLine()!.Trim();
            movie.Producer.Name = producer;

            Console.Write("First name of director: ");
            string firstName = Console.ReadLine()!.Trim().ToLower();
            if (firstName.Length > 0)
                movie.Director.FirstName = char.ToUpper(firstName[0]) + firstName.Substring(1);

            Console.Write("Last name of director: ");
            string lastName = Console.ReadLine()!.Trim().ToLower();
            if (lastName.Length > 0)
                movie.Director.LastName = char.ToUpper(lastName[0]) + lastName.Substring(1);

            string fullName = $"{firstName} {lastName}";


            var exists = await _movieGenreRepo.ExistsAsync(x => x.Genre.Name == genre);
            var existProducer = await _producerRepo.ExistsAsync(x => x.Name == producer);
            var existDirector = await _directorRepo.ExistsAsync(x => x.Fullname == fullName);
            if (exists != true)
            {
                var newGenre = new GenreEntity()
                { Name = genre };
                await _genreRepo.CreateAsync(newGenre);
            }
            
            else if (existProducer != true) 
            
            {
                var newProducer = new ProductionCompanyEntity()
                { Name = producer };
                await _producerRepo.CreateAsync(newProducer);
            }

            else if (existDirector != true)

            {
                var newDirector = new DirectorEntity()
                { FirstName = firstName, LastName = lastName };
                await _directorRepo.CreateAsync(newDirector);
            }

            else
            {
                var result = await _movieRepo.ExistsAsync(x => x.Name == movieName);
                if (result == true)
                {

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
                      
        }

        private async Task ListAllMenu()
        {
            Console.Clear();
            Console.WriteLine("Show all movies");
            Console.WriteLine("---------------------");

            var list = await _movieRepo.GetAllAsync();

            if (list != null)
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
                Console.WriteLine($"{movie.Name} - {movie.Year} - {movie.MovieGenre.Genre.Name}");
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
                        Console.WriteLine("5. Update director");
                        Console.WriteLine("0. Go back");
                        Console.Write("Choose one of the above options (0-5): ");
                        var option = Console.ReadLine();
                        Console.Clear();


                        switch (option)
                        {
                            case "1":
                                Console.Write("Enter new name: ");
                                string name = Console.ReadLine()!.Trim().ToLower();
                                if (name.Length > 0)
                                    movie.name = char.ToUpper(name[0]) + name.Substring(1);

                                Console.WriteLine("First name has now been updated!");
                                break;

                            case "2":
                                Console.Write("Enter new last name: ");
                                string lastName = Console.ReadLine()!.Trim().ToLower();
                                if (lastName.Length > 0)
                                    movie.name = char.ToUpper(lastName[0]) + lastName.Substring(1);
                                Console.Clear();
                                Console.WriteLine("Lastname has now been updated!");
                                break;

                            case "3":
                                Console.Write("Enter new birth year [xxxx]: ");
                                movie.BirthYear = int.Parse(Console.ReadLine()!.Trim());
                                Console.Clear();
                                Console.WriteLine("The birthyear has now been updated!");
                                break;

                            case "4":
                                Console.Write("Enter new death year [xxxx]: ");
                                movie.DeathYear = int.Parse(Console.ReadLine()!.Trim());
                                Console.Clear();
                                Console.WriteLine("The death year has now been updated!");
                                break;

                            case "5":
                                Console.Clear();
                                Console.Write("Enter new description: ");
                                movie.Description = Console.ReadLine();
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
            Console.Write("Firstname: ");
            var firstName = Console.ReadLine()!.Trim().ToLower();
            if (firstName.Length > 0)
                firstName = char.ToUpper(firstName[0]) + firstName.Substring(1);
            Console.Write("Lastname: ");
            var lastName = Console.ReadLine()!.Trim().ToLower();
            if (lastName.Length > 0)
                lastName = char.ToUpper(lastName[0]) + lastName.Substring(1);
            string fullName = $"{firstName} {lastName}";


            var movie = await _movieRepo.GetSpecificAsync(movie => movie.Fullname == fullName);

            if (movie != null)
            {
                Console.WriteLine();
                Console.WriteLine($"{movie.FirstName} {movie.LastName}");
                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine("Press any key to delete movie");
                Console.ReadKey();
                await _movieRepo.DeleteAsync(x => x.Fullname == fullName);
                Console.Clear();


                for (var i = 0; i < 20; i++)
                {
                    Console.Write("*");
                    Thread.Sleep(125);
                }
                Thread.Sleep(250);
                Console.Clear();
                Console.WriteLine($"The movie \"{movie.Fullname}\"  has now been deleted.");
                Thread.Sleep(2000);
                Console.Clear();
            }


            else
            {
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine($"Couldn't find any movie with the name: \"{fullName}\"");
                Console.ReadKey();
            }
        }
    }
}