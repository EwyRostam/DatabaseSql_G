using Databas_Examination_G.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Databas_Examination_G
{
    internal class Program
    {
        private static readonly string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ewyro\Nackademin\Databas_16\Databas_Examination_G\Databas_Examination_G\Contexts\Databases\DB_G.mdf;Integrated Security=True;Connect Timeout=30";
        static async Task Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args).ConfigureServices(services =>
            {
                services.AddDbContext<DataContext>(x => x.UseSqlServer(connectionString));

               
                using var sp = services.BuildServiceProvider();
                var menuService = sp.GetService<MenuService>();

            }).Build();

            await host.RunAsync();
        }
    }
}