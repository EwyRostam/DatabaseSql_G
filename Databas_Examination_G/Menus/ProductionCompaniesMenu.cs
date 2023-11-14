using Databas_Examination_G.Entities;
using Databas_Examination_G.Repositories;
using System.Diagnostics;

namespace Databas_Examination_G.Menus
{
    internal class ProductionCompaniesMenu
    {
        ProductionCompanyRepository _repo;

        public ProductionCompaniesMenu(ProductionCompanyRepository repo)
        {
            _repo = repo;
        }

        internal async Task MainMenuAsync()
        {

            var exit = false;
            do
            {
                Console.Clear();
                Console.WriteLine("1. Add a new production company");
                Console.WriteLine("2. Show all production companies");
                Console.WriteLine("3. Show details for specific production company");
                Console.WriteLine("4. Update a production company");
                Console.WriteLine("5. Delete a production company");
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
            Console.WriteLine("Add a new production company");
            Console.WriteLine("-----------------------");

            var productionCompany = new ProductionCompanyEntity(); //Instance of new production company


            Console.Write("Name of production company: ");
            string name = Console.ReadLine()!.Trim();

            var result = await _repo.ExistsAsync(x => x.Name == name);
            if (result == true)
            {
                await _repo.CreateAsync(productionCompany);
                Console.Clear();
                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine("New production company has been added.");
                Console.ReadKey();

                await MainMenuAsync();
            }
            else
            {
                Console.WriteLine("production company already exists!");
                Console.ReadKey();
                await MainMenuAsync();
            }



        }

        private async Task ListAllMenu()
        {
            Console.Clear();
            Console.WriteLine("Show all production companies");
            Console.WriteLine("---------------------");

            var list = await _repo.GetAllAsync();

            if (list != null)
            {
                foreach (var company in list) //Loop for all production companies in list
                {
                    Console.WriteLine($"{company.Name}");
                    Console.WriteLine();
                }
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("There are no production companies to show.");
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

        private async Task<ProductionCompanyEntity> ListSpecificMenu()
        {
            Console.Clear();
            Console.WriteLine("Search for the production company");
            Console.WriteLine("---------------------");
            Console.Write("Name of production company: ");
            string name = Console.ReadLine()!.Trim();

            var company = await _repo.GetSpecificAsync(company => company.Name == name); //Compares the email with the email with the companys in the list and returns the first one matching.

            if (company != null)
            {
                Console.WriteLine();
                Console.WriteLine($"{company.Name}");
                Console.ReadKey();

                return company;
            }

            else
            {
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine($"Couldn't find any production company with the name: \"{name}\"");
                Console.ReadKey();
                return null!;
            }
        }

        private async Task UpdateMenu()
        {
            try
            {
                var exit = false;

                ProductionCompanyEntity company = await ListSpecificMenu(); //Gets a production company from list through "ListSpecificMenu"

                if (company != null)
                {
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("0. Go back");
                        Console.WriteLine("1. Update name of production company");
                        Console.Write("Choose one of the above options (0-1): ");
                        var option = Console.ReadLine();
                        Console.Clear();


                        switch (option)
                        {
                            case "1":
                                Console.Write("Enter new production company name: ");
                                company.Name = Console.ReadLine()!.Trim();

                                Console.WriteLine("The production company has now been updated!");
                                break;

                            case "0":
                                exit = true;
                                break;

                            default:
                                break;
                        }

                    } while (exit == false);

                    await _repo.UpdateAsync(company);

                }
                else 
                { Console.WriteLine($"Could not find any production company with the name \"{company.Name}\""); }

            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
        }

        private async Task DeleteMenu()
        {
            Console.Clear();
            Console.WriteLine("Search for the production company you want to delete");
            Console.WriteLine("-----------------------------------------------------------");
            Console.Write("Name of production company: ");
            string name = Console.ReadLine()!.Trim().ToLower();
          

            var company = await _repo.GetSpecificAsync(company => company.Name == name);

            if (company != null)
            {
                Console.WriteLine();
                Console.WriteLine($"{company.Name}");
                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine("Press any key to delete production company");
                Console.ReadKey();
                await _repo.DeleteAsync(x => x.Name == name);
                Console.Clear();


                for (var i = 0; i < 20; i++)
                {
                    Console.Write("*");
                    Thread.Sleep(125);
                }
                Thread.Sleep(250);
                Console.Clear();
                Console.WriteLine($"The production company \"{name}\"  has now been deleted.");
                Thread.Sleep(2000);
                Console.Clear();
            }


            else
            {
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine($"Couldn't find any production company with the name: \"{name}\"");
                Console.ReadKey();
            }
        }
    }
}