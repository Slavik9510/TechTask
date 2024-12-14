using TechTask.DTO;
using TechTask.Interfaces;

namespace TechTask.Tests.Mock;
internal class CustomersServiceMock : ICustomersService
{
	private readonly List<GetCustomerDto> _customers;

	public CustomersServiceMock()
	{
		_customers = new List<GetCustomerDto>
			{
				new GetCustomerDto
				{
					FirstName = "Peter",
					LastName="Parker",
					Email = "peter.parker@example.com",
					PhoneNumber = "+1234567890"
				},
				new GetCustomerDto
				{
					FirstName = "Mary",
					LastName="Jane",
					Email = "mary.jane@example.com",
					PhoneNumber = "+1234567891"
				}
			};
	}

	public Task AddCustomerAsync(AddCustomerDto customerDto)
	{
		_customers.Add(new GetCustomerDto
		{
			FirstName = customerDto.FirstName,
			LastName = customerDto.LastName,
			Email = customerDto.Email,
			PhoneNumber = customerDto.PhoneNumber
		});

		return Task.CompletedTask;
	}

	public Task<IEnumerable<GetCustomerDto>> GetAllCustomersAsync()
	{
		return Task.FromResult<IEnumerable<GetCustomerDto>>(_customers);
	}
}
