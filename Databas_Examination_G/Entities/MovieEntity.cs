
namespace Databas_Examination_G.Entities
{
    internal class MovieEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Year { get; set;}

        public int ProducerId { get; set; }
        public ProductionCompanyEntity Producer { get; set; } = null!;

        public int DirectorId { get; set; }
        public DirectorEntity Director { get; set; } = null!;

        public int MovieGenreId { get; set; }
        public MovieGenreEntity MovieGenre { get; set; } = null!;

    }
}
/*Id int [primary key]
  Name nvarchar(50)
  Year int(4)
  Descripton nvarchar(300)
  ProducerId int
  DirectorId int*/
