using System.Linq;
using Vidly.Models;
using System.Web.Mvc;
using System.Data.Entity;

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
			return View(_Context.Movies.Include(c => c.Genre));
		}

		[Route("movies/details/{id:range(1, 99999999)}")]
		public ActionResult Details(int id) {
			var movie = _Context.Movies.Include(c => c.Genre).SingleOrDefault(c => c.Id == id); //inmediate execution + eager loading

			if (movie == null)
				return HttpNotFound();

			return View(movie);
		}
	}
}