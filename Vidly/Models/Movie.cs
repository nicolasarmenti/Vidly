using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models {
    public class Movie {
        public int Id { get; set; }

		[Required]
		[StringLength(100)]
		public string Name { get; set; }

		[Required]
		public Genre Genre { get; set; }

		[Display(Name = "Genre")]
		public byte GenreId { get; set; }

		[Display(Name = "Release Date")]
		public DateTime ReleaseDate { get; set; }

		public DateTime DateAdded { get; set; }

		[Display(Name = "Number in Stock")]
		public byte Stock { get; set; }
	}
}