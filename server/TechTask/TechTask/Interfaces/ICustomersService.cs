using TechTask.DTO;

namespace TechTask.Interfaces;

public interface ICustomersService
{
	Task AddCustomerAsync(AddCustomerDto customerDto);
	Task<IEnumerable<GetCustomerDto>> GetAllCustomersAsync();
}
