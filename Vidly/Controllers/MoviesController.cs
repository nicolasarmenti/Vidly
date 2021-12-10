using Vidly.Models;
using System.Web.Mvc;
using Vidly.ViewModels;
using System.Collections.Generic;

namespace Vidly.Controllers {
	public class MoviesController : Controller {
		// GET: Movies/Random
		public ActionResult Random() {
			var movie = new Movie() { Name = "Shrek!" };
			var customers = new List<Customer> {
				new Customer {Name = "Costumer 1" },
				new Customer {Name = "Costumer 2" }
			};

			var viewModel = new RandomMovieViewModel()
			{
				Movie = movie,
				Customers = customers
			};

			//ViewData["Movie"] = movie; //NO
			//ViewBag.Movie = movie; //NO

			return View(viewModel); // = return new ViewResult();

			//return Content("Hello World!");
			//return HttpNotFound();
			//return new EmptyResult();
			//return RedirectToAction("Index", "Home", new { page = 1, sortBy = "name" });
		}

		public ActionResult Edit(int id) {
			return Content("id=" + id);
		}

		public ActionResult Index() {
			return View(GetMovies());
		}

		[Route("movies/released/{year}/{month:regex(\\d{2}):range(1, 12)}")]
		public ActionResult ByReleaseDate(int year, int month){
			return Content(year + "/" + month);
		}

		private IEnumerable<Movie> GetMovies() {
			return new List<Movie> {
				new Movie { Id = 1, Name = "Shrek!" },
				new Movie { Id = 2, Name = "Wall-E" }
			};
		}
	}
}