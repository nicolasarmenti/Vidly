using Vidly.Models;
using System.Collections.Generic;

namespace Vidly.ViewModels {
    public class RandomMovieViewModel {
        public Movie Movie { get; set; }
        public List<Customer> Customers { get; set; }
    }
}