namespace Databas_Examination_G.Menus;

internal class MainMenu
{
    private readonly DirectorsMenu _directorsMenu;
    private readonly GenresMenu _genresMenu;
    private readonly MoviesMenu _moviesMenu;
    private readonly MovieGenresMenu _movieGenresMenu;
    private readonly ProductionCompaniesMenu _productionCompaniesMenu;

    public MainMenu(DirectorsMenu directorsMenu, GenresMenu genresMenu, MoviesMenu moviesMenu, MovieGenresMenu movieGenresMenu, ProductionCompaniesMenu productionCompaniesMenu)
    {
        _directorsMenu = directorsMenu;
        _genresMenu = genresMenu;
        _moviesMenu = moviesMenu;
        _movieGenresMenu = movieGenresMenu;
        _productionCompaniesMenu = productionCompaniesMenu;
    }


    public async Task MainMenuAsync()
    {
        do
        {
            Console.Clear();
            Console.WriteLine("1. Go To Movies");
            Console.WriteLine("2. Go To Genres");
            Console.WriteLine("3. Go To Directors");
            Console.WriteLine("4. Go To Production Companies");
            Console.WriteLine("0. Quit");
            Console.Write("Choose one option: ");
            
            var goToOption = Console.ReadLine();

            switch (goToOption)
            {
                case "1":
                    await _moviesMenu.MainMenuAsync();
                    break;
                case "2":
                    await _genresMenu.MainMenuAsync();
                    break;
                case "3":
                    await _directorsMenu.MainMenuAsync();
                    break;
                case "4":
                    await _productionCompaniesMenu.MainMenuAsync();
                    break;
                case "0":
                    break;
            }
        }
        while (true);
    }
}
