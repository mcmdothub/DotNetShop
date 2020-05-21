using DotNetShop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetShop.Data
{
    public interface IMovie
    {
        IEnumerable<Movie> GetAll();
        IEnumerable<Movie> GetPreferred(int count);
        IEnumerable<Movie> GetMoviesByCategoryId(int categoryId);
        IEnumerable<Movie> GetFilteredMovies(int id, string searchQuery);
        IEnumerable<Movie> GetFilteredMovies(string searchQuery);
        Movie GetById(int id);
        void NewMovie(Movie movie);
        void EditMovie(Movie movie);
        void DeleteMovie(int id);
    }
}