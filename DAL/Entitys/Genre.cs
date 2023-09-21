using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entitys
{
    public class Genre
    {
        public int Id { get; set; }

        public string Name { get; set; }    

        public IEnumerable<Movie> movies { get; set; } =  new  HashSet<Movie>(); 
    }
}
