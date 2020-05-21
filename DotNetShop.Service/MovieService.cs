using DotNetShop.Data;
using DotNetShop.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DotNetShop.Service
{
    public class MovieService : IMovie
    {
        private readonly ApplicationDbContext _context;

        public MovieService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void DeleteMovie(int id)
        {
            var movie = GetById(id);
            if (movie == null)
            {
                throw new ArgumentException();
            }
            _context.Remove(movie);
            _context.SaveChanges();
        }

        public void EditMovie(Movie movie)
        {
            var model = _context.Movies.First(f => f.Id == movie.Id);
            _context.Entry<Movie>(model).State = EntityState.Detached;
            _context.Update(movie);
            _context.SaveChanges();
        }
        public IEnumerable<Movie> GetAll()
        {
            return _context.Movies
                .Include(movie => movie.Category);
        }

        public Movie GetById(int id)
        {
            return GetAll().FirstOrDefault(movie => movie.Id == id);
        }

        public IEnumerable<Movie> GetFilteredMovies(int id, string searchQuery)
        {

            if (string.IsNullOrEmpty(searchQuery) || string.IsNullOrWhiteSpace(searchQuery))
            {
                return GetMoviesByCategoryId(id);
            }

            return GetFilteredMovies(searchQuery).Where(movie => movie.Category.Id == id);
        }

        public IEnumerable<Movie> GetFilteredMovies(string searchQuery)
        {
            var queries = string.IsNullOrEmpty(searchQuery) ? null : Regex.Replace(searchQuery, @"\s+", " ").Trim().ToLower().Split(" ");
            if (queries == null)
            {
                return GetPreferred(10);
            }

            return GetAll().Where(item => queries.Any(query => (item.Name.ToLower().Contains(query))));
        }

        public IEnumerable<Movie> GetMoviesByCategoryId(int categoryId)
        {
            return GetAll().Where(movie => movie.Category.Id == categoryId);
        }

        public IEnumerable<Movie> GetPreferred(int count)
        {
            return GetAll().OrderByDescending(movie => movie.Id).Where(movie => movie.IsPreferedMovie && movie.InStock != 0).Take(count);
        }

        public void NewMovie(Movie movie)
        {
            _context.Add(movie);
            _context.SaveChanges();
        }
    }
}
