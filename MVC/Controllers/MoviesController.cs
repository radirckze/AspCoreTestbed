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
    [Route("Movies")]
    public class MoviesController : Controller
    {
        private IUnitOfWork unitOfWork = null;

        public MoviesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            IGenericRepository<Movie> movieRepo = unitOfWork.MovieRepository;
            IEnumerable<Movie> movies = movieRepo.GetEntities();

            return View(movies);
        }

        [Route("")]
        [Route("Create")]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Rating")] Movie movie)
        //public IActionResult Movie(string name, int rating)
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
