using AutoMapper;
using TechTask.Controllers;
using TechTask.DTO;
using TechTask.Exceptions;
using TechTask.Interfaces;
using TechTask.Models;

namespace TechTask.Services;

public class CustomersService : ICustomersService
{
	private readonly ICustomersRepository _customersRepository;
	private readonly IMapper _mapper;
	private readonly ILogger<CustomersController> _logger;

	public CustomersService(ICustomersRepository customersRepository, IMapper mapper,
		ILogger<CustomersController> logger)
	{
		_customersRepository = customersRepository;
		_mapper = mapper;
		_logger = logger;
	}
	public async Task AddCustomerAsync(AddCustomerDto customerDto)
	{
		var customer = _mapper.Map<Customer>(customerDto);

		if (await _customersRepository.GetCustomerByEmailAsync(customer.Email) != null)
			throw new BadRequestException("A user with the specified email already exists.");

		if (await _customersRepository.GetCustomerByPhoneNumberAsync(customer.PhoneNumber) != null)
			throw new BadRequestException("A user with the specified phone number already exists.");

		await _customersRepository.AddCustomerAsync(customer);
	}

	public async Task<IEnumerable<GetCustomerDto>> GetAllCustomersAsync()
	{
		var customers = await _customersRepository.GetAllCustomersAsync();
		return _mapper.Map<List<GetCustomerDto>>(customers);
	}
}
