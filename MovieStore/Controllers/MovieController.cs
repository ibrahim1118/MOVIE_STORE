using AutoMapper;
using BLL.Implementation;
using BLL.Interface;
using DAL.Entitys;
using DAL.Spacifaction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MovieStore.Hellper;
using MovieStore.Models;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace MovieStore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MovieController : Controller
    {
        private readonly IGenreicRepositry<Movie> movieRepositry;
        private readonly IMapper mapper;
        private readonly IGenreicRepositry<Genre> genreRepositry;

        public MovieController(IGenreicRepositry<Movie> movieRepositry ,
            IMapper mapper,
            IGenreicRepositry<Genre> genreRepositry)
        {
            this.movieRepositry = movieRepositry;
            this.mapper = mapper;
            this.genreRepositry = genreRepositry;
        }
        public async Task<IActionResult> Index()
        {
            var spac = new MovieSpacifaction();
            var Movie = mapper.Map<IEnumerable<MovieVM>>(await movieRepositry.GetAllAsync(spac)); 
            return View(Movie);
        }

        public async Task<IActionResult> Add()
        {
            var spac = new Spacifaction<Genre>(); 
            var Gener = await genreRepositry.GetAllAsync(spac);
            ViewBag.Gener = new SelectList(Gener, "Id", "Name"); 
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(MovieVM model)
        {
            if (ModelState.IsValid)
            {
                if (model.Image is null)
                { ModelState.AddModelError("Image", "image is Required"); 
                  return View(model);
                }
                model.MovieImage = ImageSatting.UploadImage(model.Image, "Image");
                var movie = mapper.Map<Movie>(model); 
                await movieRepositry.AddAsync(movie);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return BadRequest();
            var spac = new MovieSpacifaction(id.Value);
            var movie = await movieRepositry.GetbyidWithApacAsync(spac);
            if (movie is null)
                return NotFound();
            var spacc = new Spacifaction<Genre>();
            var Gener = await genreRepositry.GetAllAsync(spacc);
            ViewBag.Gener = new SelectList(Gener, "Id", "Name");
            return View(mapper.Map<MovieVM>(movie)); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MovieVM model)
        {
            if (ModelState.IsValid)
            {
                if (model.Image is not null)
                {
                    ImageSatting.DeleteImage(model.MovieImage, "Image");
                    model.MovieImage = ImageSatting.UploadImage(model.Image, "Image"); 
                }
                var move  = mapper.Map<Movie>(model);
                await movieRepositry.UpdateAsync(move);
               
                return RedirectToAction("Index");
            }
            var spacc = new Spacifaction<Genre>();
            var Gener = await genreRepositry.GetAllAsync(spacc);
            ViewBag.Gener = new SelectList(Gener, "Id", "Name");
            return View(model); 
        }

        public async Task<IActionResult> Delete(int id)
        {
            var spac = new MovieSpacifaction(id);
            var move = await movieRepositry.GetbyidWithApacAsync(spac);
            await movieRepositry.DeleteAsync(move); 
            return RedirectToAction(nameof(Index)); 

        }

        
    }
}
