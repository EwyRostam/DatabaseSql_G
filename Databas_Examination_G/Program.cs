using Databas_Examination_G.Contexts;
using Databas_Examination_G.Menus;
using Databas_Examination_G.Repositories;
using Databas_Examination_G.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace Databas_Examination_G
{
    internal class Program
    {
        private static readonly string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ewyro\Nackademin\Databas_16\Databas_Examination_G\Databas_Examination_G\Contexts\Databases\DB_G.mdf;Integrated Security=True;Connect Timeout=30";
        static async Task Main(string[] args)
        {
            var services = new ServiceCollection();

            services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<DirectorsMenu>();
            services.AddScoped<GenresMenu>();
            services.AddScoped<MainMenu>();
            services.AddScoped<MoviesMenu>();
            services.AddScoped<ProductionCompaniesMenu>();
                
            services.AddScoped<DirectorRepository>();
            services.AddScoped<GenreRepository>();
            services.AddScoped<MovieRepository>();
            services.AddScoped<ProductionCompanyRepository>();
            services.AddScoped<RatingRepository>();

            services.AddScoped<DirectorService>();
            services.AddScoped<GenreService>();
            services.AddScoped<ProducerService>();
            services.AddScoped<RatingService>();
            services.AddScoped<MovieService>();




            var sp = services.BuildServiceProvider();
            var mainMenu = sp.GetRequiredService<MainMenu>();
            await mainMenu.MainMenuAsync();
        }

            
        
    }
}