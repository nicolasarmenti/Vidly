using System;
using System.Net;
using System.Linq;
using Vidly.Models;
using System.Web.Http;
using System.Data.Entity;

namespace Vidly.Controllers.Api {
    public class CustomersController : ApiController {
		private ApplicationDbContext _Context;

		public CustomersController() {
			_Context = new ApplicationDbContext();
		}

		//GET /api/customers
		public IHttpActionResult GetCustomers() {
			return Ok(_Context.Customers.Include(c => c.MembershipType).ToList());
		}

		//GET /api/customers/id
		public IHttpActionResult GetCustomers(int id) {
			var customer = _Context.Customers.SingleOrDefault(c => c.Id == id);

			if (customer == null)
				return NotFound();

			return Ok(customer);
		}

		//POST /api/customers
		[HttpPost]
		public IHttpActionResult CreateCustomer(Customer customer) {
			if (!ModelState.IsValid)
				return BadRequest();

			_Context.Customers.Add(customer);
			_Context.SaveChanges();

			return Created(new Uri(Request.RequestUri + "/" + customer.Id), customer);
		}

		//PUT /api/customers/id
		[HttpPut]
		public IHttpActionResult UpdateCustomer(int id, Customer customer) {
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

			return Ok(customer);
		}

		//DELETE /api/customers/id
		[HttpDelete]
		public IHttpActionResult DeleteCustomer(int id) {
			var customerInDB = _Context.Customers.SingleOrDefault(c => c.Id == id);

			if (customerInDB == null)
				throw new HttpResponseException(HttpStatusCode.NotFound);

			_Context.Customers.Remove(customerInDB);
			_Context.SaveChanges();

			return Ok();
		}


	}
}