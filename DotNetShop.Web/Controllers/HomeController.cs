using DotNetShop.Data;
using DotNetShop.Web.DataMapper;
using DotNetShop.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetShop.Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly IMovie _movieService;
        private readonly Mapper _mapper;

        public HomeController(IMovie movieService)
        {
            _movieService = movieService;
            _mapper = new Mapper();
        }

        [Route("/")]
        public IActionResult Index()
        {
            var preferedMovies = _movieService.GetPreferred(10);
            var model = _mapper.MoviesToHomeIndexModel(preferedMovies);
            return View(model);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Search(string searchQuery)
        {
            if (string.IsNullOrWhiteSpace(searchQuery) || string.IsNullOrEmpty(searchQuery))
            {
                return RedirectToAction("Index");
            }

            var searchedMovies = _movieService.GetFilteredMovies(searchQuery);
            var model = _mapper.MoviesToHomeIndexModel(searchedMovies);

            return View(model);
        }
    }
}
