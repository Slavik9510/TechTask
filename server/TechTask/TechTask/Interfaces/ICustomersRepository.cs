using TechTask.Models;

namespace TechTask.Interfaces;

public interface ICustomersRepository
{
	Task AddCustomerAsync(Customer customer);
	Task<IEnumerable<Customer>> GetAllCustomersAsync();
	Task<Customer?> GetCustomerByPhoneNumberAsync(string phoneNumber);
	Task<Customer?> GetCustomerByEmailAsync(string email);
}
