using TechTask.DTO;

namespace TechTask.Interfaces;

// Service interface for managing business logic related to customers
public interface ICustomersService
{
	Task AddCustomerAsync(AddCustomerDto customerDto);
	Task<IEnumerable<GetCustomerDto>> GetAllCustomersAsync();
}
