using System.ComponentModel.DataAnnotations;

namespace MovieStore.Models
{
    public class LogInVM
    {
        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

        public bool RememberMe { get; set; }    
    }
}
