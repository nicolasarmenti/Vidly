﻿using Vidly.Models;
using System.Web.Mvc;
using System.Collections.Generic;
using Vidly.ViewModels;

namespace Vidly.Controllers {
	public class MoviesController : Controller {
		// GET: Movies/Random
		public ActionResult Random() {
			var movie = new Movie() { Name = "Shrek!" };
			var customers = new List<Customer>
			{
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
			var viewModel = new MoviesViewModel {
				Movies = new List<Movie> {
					new Movie { Name = "Shrek!" },
					new Movie { Name = "Wall-E" }
				}
			};

			return View(viewModel);
		}

		[Route("movies/released/{year}/{month:regex(\\d{2}):range(1, 12)}")]
		public ActionResult ByReleaseDate(int year, int month){
			return Content(year + "/" + month);
		}
	}
}