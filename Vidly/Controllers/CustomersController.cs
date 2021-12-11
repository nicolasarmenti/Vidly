using System.Linq;
using Vidly.Models;
using System.Web.Mvc;
using System.Data.Entity;

namespace Vidly.Controllers {
	public class CustomersController : Controller {
		private ApplicationDbContext _Context;

		public CustomersController() {
			_Context = new ApplicationDbContext();
		}

		protected override void Dispose(bool disposing) {
			base.Dispose(disposing);
			_Context.Dispose();
		}

		public ViewResult Index() {
			//return View(GetCustomers());

			//var customers = _Context.Customers.ToList(); //inmediate execution
			//var customers = _Context.Customers; //defered execution
			var customers = _Context.Customers.Include(c => c.MembershipType); //defered execution + eager loading

			return View(customers);
		}

		[Route("custoemrs/details/{id:range(1, 99999999)}")]
		public ActionResult Details(int id) {
			var customer = _Context.Customers.SingleOrDefault(c => c.Id == id); //inmediate execution

			if (customer == null)
				return HttpNotFound();

			return View(customer);
		}
	}
}