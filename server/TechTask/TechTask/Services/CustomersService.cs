using AutoMapper;
using TechTask.DTO;
using TechTask.Exceptions;
using TechTask.Interfaces;
using TechTask.Models;

namespace TechTask.Services;

// Service that handles business logic related to customers
public class CustomersService : ICustomersService
{
	private readonly ICustomersRepository _customersRepository;
	private readonly IMapper _mapper;

	public CustomersService(ICustomersRepository customersRepository, IMapper mapper)
	{
		_customersRepository = customersRepository;
		_mapper = mapper;
	}

	// Adds new customer created from dto
	// Validates if email or phone number already exists, throws a BadRequestException if they do
	public async Task AddCustomerAsync(AddCustomerDto customerDto)
	{
		// Mapping to domain model from dto
		var customer = _mapper.Map<Customer>(customerDto);

		// Check if customer with the same email already exists
		if (await _customersRepository.GetCustomerByEmailAsync(customer.Email) != null)
			throw new BadRequestException("A user with the specified email already exists.");

		// Check if customer with the same phone number already exists
		if (await _customersRepository.GetCustomerByPhoneNumberAsync(customer.PhoneNumber) != null)
			throw new BadRequestException("A user with the specified phone number already exists.");

		// Add new customer to repository
		await _customersRepository.AddCustomerAsync(customer);
	}

	// Retrieves all customers
	public async Task<IEnumerable<GetCustomerDto>> GetAllCustomersAsync()
	{
		// Retrieve all customers from repository
		var customers = await _customersRepository.GetAllCustomersAsync();

		// Mapping to dtos from domain models
		return _mapper.Map<List<GetCustomerDto>>(customers);
	}
}
