using AutoMapper;
using BLL.Interface;
using DAL.Entitys;
using DAL.Spacifaction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Models;
using System.Text.RegularExpressions;

namespace MovieStore.Controllers
{
    [Authorize(Roles ="Admin")]
    public class GenreController : Controller
    {
        private readonly IGenreicRepositry<Genre> genreRepositry;
        private readonly IMapper mapper;

        public GenreController(IGenreicRepositry<Genre> genreRepositry
            ,IMapper mapper)
        {
            this.genreRepositry = genreRepositry;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var spac = new  Spacifaction<Genre>();
            var Genres = await genreRepositry.GetAllAsync(spac);
            return View(mapper.Map<IEnumerable<GenreVM>>(Genres));
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(GenreVM model)
        {
            if (ModelState.IsValid)
            {
                var genre = mapper.Map<Genre>(model);
                await genreRepositry.AddAsync (genre);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return BadRequest(); 
       
            var Genre = await genreRepositry.GetbyidAsync(id.Value); 
            if (Genre is null)
                return NotFound();
            return View(mapper.Map<GenreVM>(Genre)); 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(GenreVM model)
        {
            if (ModelState.IsValid)
            {
                var Genre = mapper.Map<Genre>(model);
                await genreRepositry.UpdateAsync(Genre);
                return RedirectToAction(nameof(Index)); 
            }
            return View(model); 
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
                return BadRequest(); 
            var Genre = await genreRepositry.GetbyidAsync(id.Value);
            await genreRepositry.DeleteAsync(Genre); 
            return RedirectToAction(nameof(Index));
        }
    }
}
