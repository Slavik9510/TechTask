using TechTask.Interfaces;
using TechTask.Models;

namespace TechTask.Tests.Mock;
internal class CustomersRepositoryMock : ICustomersRepository
{
	private readonly List<Customer> _customers;

	public CustomersRepositoryMock()
	{
		_customers = new List<Customer>
		{
			new Customer
			{
				FirstName = "Peter",
				LastName="Parker",
				Email = "peter.parker@example.com",
				PhoneNumber = "+1234567890"
			},
			new Customer
			{
				FirstName = "Mary",
				LastName="Jane",
				Email = "mary.jane@example.com",
				PhoneNumber = "+1234567891",
				Address = new Address { Street = "456 Oak St", City = "New York", Country = "USA" }
			}
		};
	}

	public Task AddCustomerAsync(Customer customer)
	{
		if (customer == null) throw new ArgumentNullException(nameof(customer));
		_customers.Add(customer);
		return Task.CompletedTask;
	}

	public Task<IEnumerable<Customer>> GetAllCustomersAsync()
	{
		return Task.FromResult<IEnumerable<Customer>>(_customers);
	}

	public Task<Customer?> GetCustomerByEmailAsync(string email)
	{
		var customer = _customers.FirstOrDefault(c => c.Email == email);
		return Task.FromResult(customer);
	}

	public Task<Customer?> GetCustomerByPhoneNumberAsync(string phoneNumber)
	{
		var customer = _customers.FirstOrDefault(c => c.PhoneNumber == phoneNumber);
		return Task.FromResult(customer);
	}
}
