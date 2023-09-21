using AutoMapper;
using BLL.Interface;
using DAL.Entitys;
using DAL.Spacifaction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Models;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace MovieStore.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGenreicRepositry<Movie> genreicRepositry;
        private readonly IMapper mapper;

        public HomeController(ILogger<HomeController> logger 
            , IGenreicRepositry<Movie> genreicRepositry
            ,IMapper mapper)
        {
            _logger = logger;
            this.genreicRepositry = genreicRepositry;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index(string term , int currentPage=1)
        {
            var movielis = new MovieList(); 
            var spac = new  MovieSpacifaction(term ,currentPage ,5);
            var spac2 = new MovieSpacifaction();
            var totalmove = await genreicRepositry.GetAllAsync(spac2);
            movielis.MoveList = mapper.Map<IEnumerable<MovieVM>>(await genreicRepositry.GetAllAsync(spac)); 
            movielis.TotalPages = totalmove.Count()/5;
            if (totalmove.Count() % 5 > 0)
                movielis.TotalPages++; 
            movielis.PageSize = 5;
            movielis.CurrentPage = currentPage; 
            return View(movielis);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var spac = new MovieSpacifaction(id);
            var move = await genreicRepositry.GetbyidWithApacAsync(spac);
            return View(mapper.Map<MovieVM>(move)); 
        }

        public IActionResult AboutUs() 
        { return View(); }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}