using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entitys
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }
        public string ReleaseYear { get; set; }

        public string MovieImage { get; set; }  // stores movie image name with extension (eg, image0001.jpg)
        [Required]
        public string Cast { get; set; }
        [Required]
        public string Director { get; set; }
       
        public int GenreId { get; set; }
        public Genre Genre { get; set; }

        public decimal Rate { get; set; }

    }
}
