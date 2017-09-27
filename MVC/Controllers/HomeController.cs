using DAL;
using DAL.Interfaces;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers
{
    
    [Route("")]
    [Route("/Home")]
    public class HomeController : Controller
    {
        private IUnitOfWork unitOfWork = null;

        public HomeController(IUnitOfWork unitOfWork) {
            this.unitOfWork = unitOfWork;
        }

        //temp code
        private List<Movie> movies = new List<Movie>()
        {
            new Movie() {Id=100, Name="Movie-1", Rating=5},
            new Movie() {Id=101, Name="Movie-2", Rating=4}

        };
        private List<Mcharacter> characters = new List<Mcharacter>()
        {
            new Mcharacter() {Id=100, Name="Character-1"},
            new Mcharacter() {Id=101, Name="Character-2"},
            new Mcharacter() {Id=102, Name="Character-3"}
        };

        [HttpGet("")]
        public IActionResult Index()
        {
            //IMovieRepository movieRepo = unitOfWork.MovieRepository;
            //IEnumerable<Movie> movies = movieRepo.GetMovies();

            ViewBag.movies = movies;
            ViewBag.characters = characters;

            return View();
        }

        [HttpGet("Temp")]
        public IActionResult Temp()
        {
            //IMovieRepository movieRepo = unitOfWork.MovieRepository;
            //IEnumerable<Movie> movies = movieRepo.GetMovies();

            ViewBag.movies = movies;

            return View("_MoviePartial");
        }

        [HttpPost("")]
        [ValidateAntiForgeryToken]
        public IActionResult Movie([Bind("Name,Rating")] Movie movie)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IGenericRepository<Movie> movieRepo = unitOfWork.MovieRepository;
                    movie = movieRepo.AddEntity(movie);
                    unitOfWork.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Something went wrong");
            }

            return RedirectToAction("Index");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
