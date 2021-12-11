using Vidly.Models;
using System.Collections.Generic;

namespace Vidly.ViewModels {
	public class NewCustomerViewModel {
		public IEnumerable<MembershipType> MembershipTypes { get; set; }
		public Customer Customer { get; set; }
	}
}