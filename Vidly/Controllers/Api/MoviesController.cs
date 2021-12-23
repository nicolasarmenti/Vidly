using System;
using System.Net;
using System.Linq;
using Vidly.Models;
using System.Web.Http;
using System.Data.Entity;

namespace Vidly.Controllers.Api {
    public class MoviesController : ApiController {
    private ApplicationDbContext _Context;

		public MoviesController() {
			_Context = new ApplicationDbContext();
		}

		//GET /api/movies
		public IHttpActionResult GetMovies(string query = null) {
			var moviesQuery = _Context.Movies
				.Include(m => m.Genre)
				.Where(m => m.NumberAvailable > 0);

			if (!string.IsNullOrWhiteSpace(query))
				moviesQuery = moviesQuery.Where(m => m.Name.Contains(query));

			return Ok(moviesQuery.ToList());
		}

		//GET /api/movies/id
		public IHttpActionResult GetMovie(int id) {
			var movie = _Context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);

			if (movie == null)
				return NotFound();

			return Ok(movie);
		}

		//POST /api/movies
		[HttpPost]
		[Authorize(Roles = RoleName.CanManageMovies)]
		public IHttpActionResult CreateMovie(Movie movie) {
			if (!ModelState.IsValid)
				return BadRequest();

			_Context.Movies.Add(movie);
			_Context.SaveChanges();

			return Created(new Uri(Request.RequestUri + "/" + movie.Id), movie);
		}

		//PUT /api/movies/id
		[HttpPut]
		[Authorize(Roles = RoleName.CanManageMovies)]
		public IHttpActionResult UpdateMovie(int id, Movie movie) {
			if (!ModelState.IsValid)
				throw new HttpResponseException(HttpStatusCode.BadRequest);

			var movieInDB = _Context.Movies.SingleOrDefault(m => m.Id == id);

			if (movieInDB == null)
				throw new HttpResponseException(HttpStatusCode.NotFound);

			movieInDB.Name = movie.Name;
			movieInDB.ReleaseDate = movie.ReleaseDate;
			movieInDB.GenreId = movie.GenreId;
			movieInDB.Stock = movie.Stock;

			_Context.SaveChanges();

			return Ok(movie);
		}

		//DELETE /api/movies/id
		[HttpDelete]
		[Authorize(Roles = RoleName.CanManageMovies)]
		public IHttpActionResult DeleteMovie(int id) {
			var movieInDB = _Context.Movies.SingleOrDefault(m => m.Id == id);

			if (movieInDB == null)
				throw new HttpResponseException(HttpStatusCode.NotFound);

			_Context.Movies.Remove(movieInDB);
			_Context.SaveChanges();

			return Ok();
		}


	}
}