using Microsoft.Build.Framework;

namespace MovieStore.Models
{
    public class GenreVM
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
