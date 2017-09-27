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
        static IUnitOfWork unitOfWork = null;
        private static IGenericRepository<Movie> movieRepo = null;
        static string jurassicPark = "Jurassic Park";
        static string jurassicWorld = "Jurassic World";
        static private Movie movieJP = null;

        [ClassInitialize]
        public static void TestInit(TestContext tc)
        {
            unitOfWork = new UnitOfWork();
            movieRepo = unitOfWork.MovieRepository;

            movieJP = movieRepo.AddEntity(new Movie() { Name = jurassicPark, Rating = 4 });
            unitOfWork.Save();
        }

        [ClassCleanup]
        public static void TestCleanup()
        {
            movieRepo.DeleteEntity(movieJP.Id);
            unitOfWork.Save();
        }

        [TestMethod]
        public void GetMovies()
        {
            IEnumerable<Movie> movies = movieRepo.GetEntities();
            Assert.IsTrue(movies != null && movies.Count() > 0);
        }

        [TestMethod]
        public void AddMovie()
        {
            Movie movie = new Movie() { Name = jurassicWorld, Rating = 4 };
            movie = movieRepo.AddEntity(movie);
            unitOfWork.Save();
            if (movie.Id > 0 )
            {
                movieRepo.DeleteEntity(movie.Id);
                unitOfWork.Save();
            }
            Assert.IsTrue(movie != null && movie.Id > 0);

        }

        [TestMethod]
        public void UpdateMovie()
        {
            string jpIII = "Jurassic Park III";
            Movie movie = new Movie() { Name = jurassicWorld, Rating = 4 };
            movie = movieRepo.AddEntity(movie);
            unitOfWork.Save();
            movie.Name = jpIII;
            movieRepo.UpdateEntity(movie);
            unitOfWork.Save();
            movie = movieRepo.GetEntityById(movie.Id);
            bool success = movie.Name.Equals(jpIII);
            movieRepo.DeleteEntity(movie.Id);
            unitOfWork.Save();

            Assert.IsTrue(success);

        }

        [TestMethod]
        public void DeleteMovie()
        {
            Movie movie = new Movie() { Name = jurassicWorld, Rating = 4 };
            movie = movieRepo.AddEntity(movie);
            unitOfWork.Save();
            bool success = movieRepo.GetEntityById(movie.Id) != null;
            int movieId = movie.Id;
            movieRepo.DeleteEntity(movie.Id);
            unitOfWork.Save();
            success = success && movieRepo.GetEntityById(movieId) == null;

            Assert.IsTrue(success);

        }

    }
}
