namespace Databas_Examination_G.Entities;

public class RatingEntity
{
    public int Id { get; set; }
    public int Rating { get; set; }

    public ICollection<MovieEntity> Movies { get; set; } = null!;
}
