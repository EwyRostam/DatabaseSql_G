namespace Databas_Examination_G.Entities;

public class ProductionCompanyEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<MovieEntity>? Movies { get; set; }
}

/*  Id int [primary key]
  Name nvarchar(50)
  Movies ICollection*/