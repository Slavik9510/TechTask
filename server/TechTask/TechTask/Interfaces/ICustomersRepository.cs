using TechTask.Models;

namespace TechTask.Interfaces;

// Repository interface for managing customer-related database operations
// Defines methods for adding and retrieving customer data from the data source
public interface ICustomersRepository
{
	Task AddCustomerAsync(Customer customer);
	Task<IEnumerable<Customer>> GetAllCustomersAsync();
	Task<Customer?> GetCustomerByPhoneNumberAsync(string phoneNumber); // null if not found
	Task<Customer?> GetCustomerByEmailAsync(string email); // null if not found
}
