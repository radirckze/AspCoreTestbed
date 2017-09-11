using DAL.Model;
using DAL.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{

    public class MovieRepository : IMovieRepository, IDisposable {

        private MovieBuffContext context = null;
        private bool disposed = false;

        public MovieRepository(MovieBuffContext context) {
            if (context == null) {
                throw new ArgumentException("missing context");
            }

            this.context = context;
        }

        public IEnumerable<Movie> GetMovies() {
            try {
                return context.Movie.ToList();
            }
            catch(Exception ex) {
                //log error
                throw ex;
            }
        }

        public Movie GetMovieByName(string movieName)
        {
            try
            {
                return context.Movie.FirstOrDefault(a => a.Name == movieName);
            }
            catch (Exception ex)
            {
                // do someting
                throw ex;
            }
        }

        public Movie AddMovie(Movie movie)
        {
            try
            {
                context.Movie.Add(movie);
                return movie;
            }
            catch (Exception ex)
            {
                //do someting
                throw ex;
            }
        }


        public void DeleteMovie(int movieId)
        {
            Movie movie = context.Movie.Find(movieId);
            if (movie != null)
            {
                context.Movie.Remove(movie);
            }
        }

        public void UpdateMovie(Movie movie)
        {
            context.Entry(movie).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
        
        //public void Save() {

        //    context.SaveChanges();
        //}

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
    
}