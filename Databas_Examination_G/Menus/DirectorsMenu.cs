using Databas_Examination_G.Entities;
using Databas_Examination_G.Repositories;
using Databas_Examination_G.Services;
using System.Diagnostics;

namespace Databas_Examination_G.Menus
{
    internal class DirectorsMenu
    {
        private readonly DirectorService _service;

        public DirectorsMenu(DirectorService service)
        {
            _service = service;
        }

        internal async Task MainMenuAsync()
        {
           
                var exit = false;
                do
                {
                    Console.Clear();
                    Console.WriteLine("1. Create new director");
                    Console.WriteLine("2. Show all directors");
                    Console.WriteLine("3. Show details for specific director");
                    Console.WriteLine("4. Update a director");
                    Console.WriteLine("5. Delete a director");
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
            Console.WriteLine("Add a new director");
            Console.WriteLine("-----------------------");

            var director = new DirectorEntity(); //Instance of new director


            Console.Write("First name: ");
            string firstName = Console.ReadLine()!.Trim().ToLower();
            if (firstName.Length > 0)
                director.FirstName = char.ToUpper(firstName[0]) + firstName.Substring(1); //Makes first letter big if the name is longes than one letter

            Console.Write("Last name: ");
            string lastName = Console.ReadLine()!.Trim().ToLower();
            if (lastName.Length > 0)
                director.LastName = char.ToUpper(lastName[0]) + lastName.Substring(1);

            Console.Write("Description: ");
            director.Description = Console.ReadLine()!.Trim();

            Console.Write("Year of birth [xxxx]: ");
            director.BirthYear = int.Parse(Console.ReadLine()!.Trim());

            Console.Write("Year of death [xxxx] (optional): ");
            director.DeathYear = int.Parse(Console.ReadLine()!.Trim());

            var result = await _service.CreateDirectorAsync(director);
            if (result != null)
            {
                Console.Clear();
                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine("The director has been added!");
                Console.ReadKey();

                await MainMenuAsync();
            }
            else
            {
                Console.WriteLine("The director already exists!");
                Console.ReadKey();
                await MainMenuAsync();
            }
        }

        private async Task ListAllMenu()
        {
            Console.Clear();
            Console.WriteLine("Show all directors");
            Console.WriteLine("---------------------");

            var list = await _service.GetAllAsync();

            if (list != null)
            {
                foreach (var director in list) //Loop for all directors in list
                {
                    Console.WriteLine($"{director.FirstName} {director.LastName}");

                }
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("There are no directors to show.");
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

        private async Task<DirectorEntity> ListSpecificMenu()
        {
            Console.Clear();
            Console.WriteLine("Search for the director");
            Console.WriteLine("---------------------");
            Console.Write("Firstname: ");
            var firstName = Console.ReadLine()!.Trim().ToLower();
            if (firstName.Length > 0)
                firstName = char.ToUpper(firstName[0]) + firstName.Substring(1);
            Console.Write("Lastname: ");
            var lastName = Console.ReadLine()!.Trim().ToLower();
            if (lastName.Length > 0)
                lastName = char.ToUpper(lastName[0]) + lastName.Substring(1);
            string fullName = $"{firstName} {lastName}";


            var director = await _service.GetSpecificAsync(firstName, lastName); //Compares the email with the email with the directors in the list and returns the first one matching.

            if (director != null)
            {
                Console.Clear();
                Console.WriteLine($"{director.FirstName} {director.LastName}");
                Console.WriteLine($"{ director.BirthYear} - { director.DeathYear}");
                Console.WriteLine($"{director.Description}");
                Console.WriteLine();
                Console.ReadKey();
                if (director.Movies != null)
                {
                    Console.WriteLine($"Movies:");
                    foreach (var movie in director.Movies!)
                    {
                        Console.WriteLine($"{movie.Name}");
                    }
                    Console.ReadKey();
                    
                }
                
                return director;
            }

            else
            {
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine($"Couldn't find any director with the name: \"{fullName}\"");
                Console.ReadKey();
                return null!;
            }
        }

        private async Task UpdateMenu()
        {
            try
            {
                DirectorEntity director = await ListSpecificMenu(); //Gets a director from list through "ListSpecificMenu"
                
                var exit = false;

                if (director != null)
                {
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("1. Update first name");
                        Console.WriteLine("2. Update last name");
                        Console.WriteLine("3. Update birth year");
                        Console.WriteLine("4. Update death year");
                        Console.WriteLine("5. Update description");
                        Console.WriteLine("0. Go back");
                        Console.Write("Choose one of the above options (0-5): ");
                        var option = Console.ReadLine();
                        Console.Clear();


                        switch (option)
                        {
                            case "1":
                                Console.Write("Enter new first name: ");
                                string firstName = Console.ReadLine()!.Trim().ToLower();
                                if (firstName.Length > 0)
                                    director.FirstName = char.ToUpper(firstName[0]) + firstName.Substring(1);

                                Console.WriteLine("First name has now been updated!");
                                break;

                            case "2":
                                Console.Write("Enter new last name: ");
                                string lastName = Console.ReadLine()!.Trim().ToLower();
                                if (lastName.Length > 0)
                                    director.FirstName = char.ToUpper(lastName[0]) + lastName.Substring(1);
                                Console.Clear();
                                Console.WriteLine("Lastname has now been updated!");
                                break;

                            case "3":
                                Console.Write("Enter new birth year [xxxx]: ");
                                director.BirthYear = int.Parse(Console.ReadLine()!.Trim());
                                Console.Clear();
                                Console.WriteLine("The birthyear has now been updated!");
                                break;

                            case "4":
                                Console.Write("Enter new death year [xxxx]: ");
                                director.DeathYear = int.Parse(Console.ReadLine()!.Trim());
                                Console.Clear();
                                Console.WriteLine("The death year has now been updated!");
                                break;

                            case "5":
                                Console.Clear();
                                Console.Write("Enter new description: ");
                                director.Description = Console.ReadLine();
                                break;

                            case "0":
                                exit = true;
                                break;

                            default:
                                break;
                        }

                    } while (exit == false);

                    await _service.UpdateAsync(director);

                }

            } catch (Exception ex) { Debug.WriteLine(ex.Message); }
        }

        private async Task DeleteMenu()
        {
            Console.Clear();
            Console.WriteLine("Search for the director you want to delete");
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


            var director = await _service.GetSpecificAsync(firstName, lastName);

            if (director != null)
            {
                Console.WriteLine();
                Console.WriteLine($"{director.FirstName} {director.LastName}");
                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine("Press any key to delete director");
                Console.ReadKey();
                await _service.DeleteAsync(firstName, lastName);
                Console.Clear();


                for (var i = 0; i < 20; i++)
                {
                    Console.Write("*");
                    Thread.Sleep(125);
                }
                Thread.Sleep(250);
                Console.Clear();
                Console.WriteLine($"The director \"{fullName}\"  has now been deleted.");
                Thread.Sleep(2000);
                Console.Clear();
            }


            else
            {
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine($"Couldn't find any director with the name: \"{fullName}\"");
                Console.ReadKey();
            }
        }

       

       

        

       
    }
}