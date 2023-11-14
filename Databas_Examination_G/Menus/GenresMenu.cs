using Databas_Examination_G.Entities;
using Databas_Examination_G.Repositories;
using System.Diagnostics;

namespace Databas_Examination_G.Menus
{
    internal class GenresMenu
    {
        private readonly GenreRepository _repo;

        public GenresMenu(GenreRepository repo)
        {
            _repo = repo;
        }

        internal async Task MainMenuAsync()
        {

            var exit = false;
            do
            {
                Console.Clear();
                Console.WriteLine("1. Create new genre");
                Console.WriteLine("2. Show all genres");
                Console.WriteLine("3. Show details for specific genre");
                Console.WriteLine("4. Update a genre");
                Console.WriteLine("5. Delete a genre");
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
            Console.WriteLine("Add a new genre");
            Console.WriteLine("-----------------------");

            var genre = new GenreEntity(); //Instance of new genre


            Console.Write("Name of genre: ");
            string genreName = Console.ReadLine()!.Trim().ToLower();
            if (genreName.Length > 0)
                genre.Name = char.ToUpper(genreName[0]) + genreName.Substring(1); //Makes genre letter big if the name is longes than one letter

            var result = await _repo.ExistsAsync(x => x.Name == genreName);
            if (result == true)
            {
                await _repo.CreateAsync(genre);
                Console.Clear();
                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine("New genre has been added.");
                Console.ReadKey();

                await MainMenuAsync();
            }
            else
            {
                Console.WriteLine("Genre already exists!");
                Console.ReadKey();
                await MainMenuAsync();
            }
           

           
        }

        private async Task ListAllMenu()
        {
            Console.Clear();
            Console.WriteLine("Show all genres");
            Console.WriteLine("---------------------");

            var list = await _repo.GetAllAsync();

            if (list != null)
            {
                foreach (var genre in list) //Loop for all genres in list
                {
                    Console.WriteLine($"{genre.Name}");
                    Console.WriteLine();
                }
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("There are no genres to show.");
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

        private async Task<GenreEntity> ListSpecificMenu()
        {
            Console.Clear();
            Console.WriteLine("Search for the genre");
            Console.WriteLine("---------------------");
            Console.Write("Name of genre: ");
            string genreName = Console.ReadLine()!.Trim().ToLower();
            if (genreName.Length > 0)
                genreName = char.ToUpper(genreName[0]) + genreName.Substring(1);


            var genre = await _repo.GetSpecificAsync(genre => genre.Name == genreName); //Compares the email with the email with the genres in the list and returns the first one matching.

            if (genre != null)
            {
                Console.WriteLine();
                Console.WriteLine($"{genre.Name}");
                Console.ReadKey();
                
                return genre;
            }

            else
            {
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine($"Couldn't find any genre with the name: \"{genreName}\"");
                Console.ReadKey();
                return null!;
            }
        }

        private async Task UpdateMenu()
        {
            try
            {
                var exit = false;

                GenreEntity genre = await ListSpecificMenu(); //Gets a genre from list through "ListSpecificMenu"

                if (genre != null)
                {
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("0. Go back");
                        Console.WriteLine("1. Update name of genre");
                        Console.Write("Choose one of the above options (0-1): ");
                        var option = Console.ReadLine();
                        Console.Clear();


                        switch (option)
                        {
                            case "1":
                                Console.Write("Enter new genre name: ");
                                string genreName = Console.ReadLine()!.Trim().ToLower();
                                if (genreName.Length > 0)
                                    genre.Name = char.ToUpper(genreName[0]) + genreName.Substring(1);

                                Console.WriteLine("The genre has now been updated!");
                                break;

                            case "0":
                                exit = true;
                                break;

                            default:
                                break;
                        }

                    } while (exit == false);

                    await _repo.UpdateAsync(genre);

                }

            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
        }

        private async Task DeleteMenu()
        {
            Console.Clear();
            Console.WriteLine("Search for the genre you want to delete");
            Console.WriteLine("------------------------------------");
            Console.Write("Firstname: ");
            Console.Write("Name of genre: ");
            string genreName = Console.ReadLine()!.Trim().ToLower();
            if (genreName.Length > 0)
                genreName = char.ToUpper(genreName[0]) + genreName.Substring(1);


            var genre = await _repo.GetSpecificAsync(genre => genre.Name == genreName);

            if (genre != null)
            {
                Console.WriteLine();
                Console.WriteLine($"{genre.Name}");
                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine("Press any key to delete genre");
                Console.ReadKey();
                await _repo.DeleteAsync(x => x.Name == genreName);
                Console.Clear();


                for (var i = 0; i < 20; i++)
                {
                    Console.Write("*");
                    Thread.Sleep(125);
                }
                Thread.Sleep(250);
                Console.Clear();
                Console.WriteLine($"The genre \"{genre.Name}\"  has now been deleted.");
                Thread.Sleep(2000);
                Console.Clear();
            }


            else
            {
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine($"Couldn't find any genre with the name: \"{genreName}\"");
                Console.ReadKey();
            }
        }

    }
}