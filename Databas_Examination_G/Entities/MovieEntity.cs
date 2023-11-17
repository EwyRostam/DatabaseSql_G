

namespace Databas_Examination_G.Entities
{
    public class MovieEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int Year { get; set;}

        public int ProducerId { get; set; }
        public ProductionCompanyEntity Producer { get; set; } = null!;

        public int DirectorId { get; set; }
        public DirectorEntity Director { get; set; } = null!;

        public int GenreId { get; set; }
        public GenreEntity Genre { get; set; } = null!;

        public int RatingId { get; set; }
        public RatingEntity? Rating { get; set; } 
       



    }
}
/*Id int [primary key]
  Name nvarchar(50)
  Year int(4)
  Descripton nvarchar(300)
  ProducerId int
  DirectorId int*/
