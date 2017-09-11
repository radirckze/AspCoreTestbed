using DAL;
using DAL.Model;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;

namespace DAL.Test
{
    [TestClass]
    public class MovieTest
    {
        //private MovieBuffContext context = null;
        IUnitOfWork unitOfWork = null;
        private IMovieRepository movieRepo = null;
        string jurassicPark = "Jurassic Park";
        string jurassicWorld = "Jurassic World";
        private Movie movieJP = null;

        [TestInitialize]
        public void TestInit()
        {
            unitOfWork = new UnitOfWork();
            movieRepo = unitOfWork.MovieRepository;

            movieJP = movieRepo.AddMovie(new Movie() { Name = jurassicPark, Rating = 4 });
            unitOfWork.Save();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            movieRepo.DeleteMovie(movieJP.Id);
            unitOfWork.Save();
        }

        [TestMethod]
        public void GetMovies()
        {
            IEnumerable<Movie> movies = movieRepo.GetMovies();
            Assert.IsTrue(movies != null && movies.Count() > 0);
        }

        [TestMethod]
        public void GetMovieByName()
        {
            Movie movie = movieRepo.GetMovieByName(jurassicPark);
            Assert.IsTrue(movie != null && movie.Name.Equals(jurassicPark)) ;
        }

        [TestMethod]
        public void AddMovie()
        {
            Movie movie = new Movie() { Name = jurassicWorld, Rating = 4 };
            movieRepo.AddMovie(movie);
            unitOfWork.Save();
            movie = movieRepo.GetMovieByName(jurassicWorld);
            if (movie.Id > 0 )
            {
                movieRepo.DeleteMovie(movie.Id);
                unitOfWork.Save();
            }
            Assert.IsTrue(movie != null && movie.Id > 0);

        }

        [TestMethod]
        public void UpdateMovie()
        {
            string jpIII = "Jurassic Park III";
            Movie movie = new Movie() { Name = jurassicWorld, Rating = 4 };
            movie = movieRepo.AddMovie(movie);
            unitOfWork.Save();
            movie.Name = jpIII;
            movieRepo.UpdateMovie(movie);
            unitOfWork.Save();
            bool success = movieRepo.GetMovieByName(jpIII) != null ? true : false ;
            movieRepo.DeleteMovie(movie.Id);
            unitOfWork.Save();

            Assert.IsTrue(success);

        }

        [TestMethod]
        public void DeleteMovie()
        {
            Movie movie = new Movie() { Name = jurassicWorld, Rating = 4 };
            movie = movieRepo.AddMovie(movie);
            unitOfWork.Save();
            bool success = movieRepo.GetMovieByName(jurassicWorld) != null ? true : false;
            movieRepo.DeleteMovie(movie.Id);
            unitOfWork.Save();
            success = success && movieRepo.GetMovieByName(jurassicWorld) == null ? true : false;

            Assert.IsTrue(success);

        }

    }
}
