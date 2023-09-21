using DAL.Entitys;
using System.ComponentModel.DataAnnotations;

namespace MovieStore.Models
{
    public class MovieVM
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }
        [Range(1800 , 2024)]
        public int ReleaseYear { get; set; }

        public string? MovieImage { get; set; }  // stores movie image name with extension (eg, image0001.jpg)
     
        public IFormFile? Image { get; set; }
        public string Cast { get; set; }
        [Required]
        public string Director { get; set; }

        [Range(1 , 10)]
        public decimal Rate { get; set; }
        public int GenreId { get; set; }
        public Genre? Genre { get; set; }
    }
}
