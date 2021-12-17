using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models {
    public class Movie {
        public int Id { get; set; }

		[Required]
		[StringLength(100)]
		public string Name { get; set; }

		public Genre Genre { get; set; }

		[Required]
		[Display(Name = "Genre")]
		public byte GenreId { get; set; }

		[Required]
		[Display(Name = "Release Date")]
		public DateTime? ReleaseDate { get; set; }

		public DateTime DateAdded { get; set; }

		[Required]
		[Range(1, 20)]
		[Display(Name = "Number in Stock")]
		public byte? Stock { get; set; }
	}
}