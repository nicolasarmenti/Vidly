using System;
using System.Linq;
using Vidly.Models;
using System.Web.Mvc;
using Vidly.ViewModels;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace Vidly.Controllers {
	public class MoviesController : Controller {
		private ApplicationDbContext _Context;

		public MoviesController() {
			_Context = new ApplicationDbContext();
		}

		protected override void Dispose(bool disposing) {
			base.Dispose(disposing);
			_Context.Dispose();
		}

		public ActionResult Index() {
			if (User.IsInRole(RoleName.CanManageMovies))
				return View("List");
			else
				return View("ReadOnlyList");
		}

		[Route("movies/details/{id:range(1, 99999999)}")]
		public ActionResult Details(int id) {
			var movie = _Context.Movies.Include(c => c.Genre).SingleOrDefault(c => c.Id == id); //inmediate execution + eager loading

			if (movie == null)
				return HttpNotFound();

			return View(movie);
		}

		[Authorize(Roles = RoleName.CanManageMovies)]
		public ActionResult New() {
			var viewModel = new MovieFormViewModel() {
				Movie = new Movie(),
				Genres = _Context.Genres.ToList()
			};

			return View("MovieForm", viewModel);
		}

		[HttpPost]
		[Authorize(Roles = RoleName.CanManageMovies)]
		public ActionResult Save(Movie movie) {
			if (!ModelState.IsValid)
				return View("MovieForm", new MovieFormViewModel { Movie = movie, Genres = _Context.Genres });

			if (movie.Id == 0) {
				//no exite (insert)
				movie.DateAdded = DateTime.Now;
				_Context.Movies.Add(movie);
			} else {
				//existe de antes (edit)
				var movieInDB = _Context.Movies.Single(m => m.Id == movie.Id);
				//TryUpdateModel(customerInDB); //no usar
				movieInDB.Name = movie.Name;
				movieInDB.ReleaseDate = movie.ReleaseDate;
				movieInDB.GenreId = movie.GenreId;
				movieInDB.Stock = movie.Stock;
			}

			_Context.SaveChanges();
			return RedirectToAction("Index", "Movies");
		}

		[Authorize(Roles = RoleName.CanManageMovies)]
		public ActionResult Edit(int id) {
			var movie = _Context.Movies.SingleOrDefault(m => m.Id == id);

			if (movie == null)
				return HttpNotFound();

			var viewModel = new MovieFormViewModel() {
				Movie = movie,
				Genres = _Context.Genres.ToList()
			};

			return View("MovieForm", viewModel);
		}
	}
}