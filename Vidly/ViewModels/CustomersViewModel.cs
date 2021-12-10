using Vidly.Models;
using System.Collections.Generic;

namespace Vidly.ViewModels {
	public class CustomersViewModel {
		public IEnumerable<Customer> Customers { get; set; }
	}
}