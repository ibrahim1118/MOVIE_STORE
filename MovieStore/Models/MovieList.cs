using DAL.Entitys;

namespace MovieStore.Models
{
    public class MovieList
    {
        public IEnumerable<MovieVM> MoveList { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string? Term { get; set; }
    }
}
