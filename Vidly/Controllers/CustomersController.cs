using System.Linq;
using Vidly.Models;
using System.Web.Mvc;
using System.Collections.Generic;

namespace Vidly.Controllers {
	public class CustomersController : Controller {
		public ViewResult Index() {
			return View(GetCustomers());
		}

		[Route("custoemrs/details/{id:range(1, 99999999)}")]
		public ActionResult Details(int id) {
			var customer = GetCustomers().SingleOrDefault(c => c.Id == id);

			if (customer == null)
				return HttpNotFound();

			return View(customer);
		}

		private IEnumerable<Customer> GetCustomers() {
			return new List<Customer> {
				new Customer { Id = 1, Name = "John Smith" },
				new Customer { Id = 2, Name = "Mary Williams" }
			};	
		}
	}
}