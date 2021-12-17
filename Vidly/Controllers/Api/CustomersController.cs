using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;

namespace Vidly.Controllers.Api {
    public class CustomersController : ApiController {
		private ApplicationDbContext _Context;

		public CustomersController() {
			_Context = new ApplicationDbContext();
		}

		//GET /api/customers
		public IEnumerable<Customer> GetCustomers() {
			return _Context.Customers.ToList();
		}

		//GET /api/customers/id
		public Customer GetCustomers(int id) {
			var customer = _Context.Customers.SingleOrDefault(c => c.Id == id);

			if (customer == null)
				throw new HttpResponseException(HttpStatusCode.NotFound);

			return customer;
		}

		//POST /api/customers
		[HttpPost]
		public Customer CreateCustomer(Customer customer) {
			if (!ModelState.IsValid)
				throw new HttpResponseException(HttpStatusCode.BadRequest);

			_Context.Customers.Add(customer);
			_Context.SaveChanges();

			return customer;
		}

		//PUT /api/customers/id
		[HttpPut]
		public void UpdateCustomer(int id, Customer customer) {
			if (!ModelState.IsValid)
				throw new HttpResponseException(HttpStatusCode.BadRequest);

			var customerInDB = _Context.Customers.SingleOrDefault(c => c.Id == id);

			if (customerInDB == null)
				throw new HttpResponseException(HttpStatusCode.NotFound);

			customerInDB.Name = customer.Name;
			customerInDB.BirthDate = customer.BirthDate;
			customerInDB.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
			customerInDB.MembershipTypeId = customer.MembershipTypeId;

			_Context.SaveChanges();
		}

		//DELETE /api/customers/id
		[HttpDelete]
		public void DeleteCustomer(int id) {
			var customerInDB = _Context.Customers.SingleOrDefault(c => c.Id == id);

			if (customerInDB == null)
				throw new HttpResponseException(HttpStatusCode.NotFound);

			_Context.Customers.Remove(customerInDB);
			_Context.SaveChanges();
		}


	}
}