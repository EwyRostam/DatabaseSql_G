namespace Databas_Examination_G.Entities;

public class GenreEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<MovieEntity> Movies { get; set; } = new HashSet<MovieEntity>();
}
