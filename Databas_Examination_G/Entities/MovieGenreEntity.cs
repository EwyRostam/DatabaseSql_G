using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Databas_Examination_G.Entities;

public class MovieGenreEntity
{
  
    [Key, Column(Order = 0)]
    public int MovieId { get; set; }
    public MovieEntity Movie { get; set; } = null!;

    [Key, Column(Order = 1)]
    public int GenreId { get; set; }
    public GenreEntity Genre { get; set; } = null!;
}
