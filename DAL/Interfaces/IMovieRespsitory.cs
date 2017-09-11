using DAL.Model;

using System;
using System.Collections.Generic;

namespace DAL.Interfaces
{

    public interface IMovieRepository {

        IEnumerable<Movie> GetMovies();
        Movie GetMovieByName(string movieName);
        Movie AddMovie(Movie movie);
        void UpdateMovie(Movie movie);
        void DeleteMovie(int movieId);
        void Save();
    }
    
}