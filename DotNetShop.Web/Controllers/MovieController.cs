using DotNetShop.Data;
using DotNetShop.Data.Models;
using DotNetShop.Web.DataMapper;
using DotNetShop.Web.Models.Category;
using DotNetShop.Web.Models.Movie;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetShop.Web.Controllers
{
	public class MovieController : Controller
	{
		private readonly ICategory _categoryService;
		private readonly IMovie _movieService;
		private readonly Mapper _mapper;

		public MovieController(ICategory categoryService, IMovie movieService)
		{
			_categoryService = categoryService;
			_movieService = movieService;
			_mapper = new Mapper();
		}

		[Route("Movies/{id}")]
		public IActionResult Index(int id)
		{
			var movie = _movieService.GetById(id);

			var model = new MovieIndexModel
			{
				Id = movie.Id,
				Name = movie.Name,
				ImageUrl = movie.ImageUrl,
				InStock = movie.InStock,
				Price = movie.Price,
				Description = movie.ShortDescription + "\n" + movie.LongDescription,
				CategoryId = movie.Category.Id,
				CategoryName = movie.Category.Name
			};

			return View(model);
		}

		[Authorize(Roles = "Admin")]
		public IActionResult New(int categoryId = 0)
		{
			GetCategoriesForDropDownList();
			NewMovieModel model = new NewMovieModel
			{
				CategoryId = categoryId
			};

			ViewBag.ActionText = "create";
			ViewBag.Action = "New";
			ViewBag.CancelAction = "Topic";
			ViewBag.SubmitText = "Create Movie";
			ViewBag.RouteId = categoryId;
			ViewBag.ControllerName = "Category";

			if (categoryId == 0)
			{
				ViewBag.CancelAction = "Index";
				ViewBag.ControllerName = "Home";
			}

			return View("CreateEdit", model);
		}

		[HttpPost]
		[Authorize(Roles = "Admin")]
		public IActionResult New(NewMovieModel model)
		{
			if (ModelState.IsValid && _categoryService.GetById(model.CategoryId.Value) != null)
			{
				var movie = _mapper.NewMovieModelToMovie(model, true, _categoryService);
				_movieService.NewMovie(movie);
				return RedirectToAction("Index", new { id = movie.Id });
			}
			GetCategoriesForDropDownList();

			ViewBag.ActionText = "create";
			ViewBag.Action = "New";
			ViewBag.ControllerName = "Category";
			ViewBag.CancelAction = "Topic";
			ViewBag.SubmitText = "Create Movie";
			ViewBag.RouteId = model.CategoryId;

			return View("CreateEdit", model);
		}

		[Authorize(Roles = "Admin")]
		public IActionResult Edit(int id)
		{
			ViewBag.ActionText = "change";
			ViewBag.Action = "Edit";
			ViewBag.CancelAction = "Index";
			ViewBag.SubmitText = "Save Changes";
			ViewBag.ControllerName = "Movie";
			ViewBag.RouteId = id;

			GetCategoriesForDropDownList();

			var movie = _movieService.GetById(id);
			if (movie != null)
			{
				var model = _mapper.MovieToNewMovieModel(movie);
				return View("CreateEdit", model);
			}

			return View("CreateEdit");
		}

		[HttpPost]
		[Authorize(Roles = "Admin")]
		public IActionResult Edit(NewMovieModel model)
		{
			if (ModelState.IsValid)
			{
				var movie = _mapper.NewMovieModelToMovie(model, false, _categoryService);
				_movieService.EditMovie(movie);
				return RedirectToAction("Index", new { id = model.Id });
			}

			ViewBag.ActionText = "change";
			ViewBag.Action = "Edit";
			ViewBag.CancelAction = "Index";
			ViewBag.SubmitText = "Save Changes";
			ViewBag.ControllerName = "Movie";
			ViewBag.RouteId = model.Id;
			GetCategoriesForDropDownList();

			return View("CreateEdit", model);
		}

		[Authorize(Roles = "Admin")]
		public IActionResult Delete(int id)
		{
			var categoryId = _movieService.GetById(id).CategoryId;
			_movieService.DeleteMovie(id);

			return RedirectToAction("Topic", "Category", new { id = categoryId, searchQuery = "" });
		}

		private void GetCategoriesForDropDownList()
		{
			var categories = _categoryService.GetAll().Select(category => new CategoryDropdownModel
			{
				Id = category.Id,
				Name = category.Name
			});
			ViewBag.Categories = new SelectList(categories, "Id", "Name");
		}
	}
}
