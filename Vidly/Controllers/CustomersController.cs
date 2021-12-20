using System.Linq;
using Vidly.Models;
using System.Web.Mvc;
using Vidly.ViewModels;
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
			//var customers = _Context.Customers.Include(c => c.MembershipType); //defered execution + eager loading

			return View();
		}

		[Route("customers/details/{id:range(1, 99999999)}")]
		public ActionResult Details(int id) {
			var customer = _Context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id); //inmediate execution + eager loading

			if (customer == null)
				return HttpNotFound();

			return View(customer);
		}

		public ActionResult New() {
			var viewModel = new CustomerFormViewModel() {
				Customer = new Customer(),
				MembershipTypes = _Context.MembershipTypes
			};

			return View("CustomerForm", viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Save(Customer customer) {
			if (!ModelState.IsValid)
				return View("CustomerForm", new CustomerFormViewModel { Customer = customer, MembershipTypes = _Context.MembershipTypes });

			if (customer.Id == 0) {
				//no exite (insert)
				_Context.Customers.Add(customer);
			} else {
				//existe de antes (edit)
				var customerInDB = _Context.Customers.Single(c => c.Id == customer.Id);
				//TryUpdateModel(customerInDB); //no usar
				customerInDB.Name = customer.Name;
				customerInDB.BirthDate= customer.BirthDate;
				customerInDB.MembershipTypeId= customer.MembershipTypeId;
				customerInDB.IsSubscribedToNewsLetter= customer.IsSubscribedToNewsLetter;
			}
			_Context.SaveChanges();

			return RedirectToAction("Index", "Customers");
		}

		public ActionResult Edit(int id) {
			var customer = _Context.Customers.SingleOrDefault(c => c.Id == id);

			if (customer == null)
				return HttpNotFound();

			var viewModel = new CustomerFormViewModel() {
				Customer = customer,
				MembershipTypes = _Context.MembershipTypes.ToList()
			};

			return View("CustomerForm", viewModel);
		}
	}
}