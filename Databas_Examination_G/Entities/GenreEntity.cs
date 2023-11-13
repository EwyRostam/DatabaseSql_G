namespace Databas_Examination_G.Entities;

internal class GenreEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public int MovieGenreId { get; set; }
    public MovieGenreEntity MovieGenre { get; set; } = null!;
}
