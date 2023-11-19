
namespace Databas_Examination_G.Models;

internal class MovieRegistration
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int Year { get; set; }
    public string DirectorFirstName { get; set; } = null!;
    public string DirectorLastName { get; set; } = null!;
    public string GenreName { get; set; } = null!;
    public string ProducerName { get; set; } = null!;
    public int Rating { get; set; }
}
