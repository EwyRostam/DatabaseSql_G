using System.ComponentModel.DataAnnotations.Schema;

namespace Databas_Examination_G.Entities;

internal class DirectorEntity
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    public string Fullname => $"{FirstName} {LastName}";
    public int? BirthYear { get; set; }

    public int? DeathYear { get; set; }

    [Column(TypeName = "nvarchar(300)")]
    public string? Description { get; set; }
    public ICollection<MovieEntity>? Movies { get; set; }
}
    



/* Id int [primary key]
  FirstName nvarchar(50)
  LastName nvarchar(50)
  Movies ICollection*/