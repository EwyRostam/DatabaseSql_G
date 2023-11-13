using Databas_Examination_G.Entities;
using Databas_Examination_G.Repositories;

namespace Databas_Examination_G.Menus
{
    internal class DirectorsMenu
    {
        private readonly DirectorRepository _repo;

        public DirectorsMenu(DirectorRepository repo)
        {
            _repo = repo;
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

            await _repo.CreateAsync(director);

            Console.Clear();
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("New director has been added.");
            Console.ReadKey();

            await MainMenuAsync();
        }

        private async Task ListAllMenu()
        {
            Console.Clear();
            Console.WriteLine("Show all directors");
            Console.WriteLine("---------------------");

            var list = await _repo.GetAllAsync();

            if (list != null)
            {
                foreach (var director in list) //Loop for all directors in list
                {
                    Console.WriteLine($"{director.FirstName} {director.LastName}");
                    Console.WriteLine();
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
        private async Task DeleteMenu()
        {
            throw new NotImplementedException();
        }

        private async Task UpdateMenu()
        {
            throw new NotImplementedException();
        }

        private async Task ListSpecificMenu()
        {
            throw new NotImplementedException();
        }

        

       
    }
}