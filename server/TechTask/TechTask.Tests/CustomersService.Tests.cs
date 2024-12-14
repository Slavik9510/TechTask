using AutoMapper;
using System.ComponentModel.DataAnnotations;
using TechTask.DTO;
using TechTask.Exceptions;
using TechTask.Interfaces;
using TechTask.Mapping;
using TechTask.Services;
using TechTask.Tests.Mock;

namespace TechTask.Tests;
public class CustomersServiceTests
{
	private readonly CustomersService _service;
	private readonly ICustomersRepository _repository;

	public CustomersServiceTests()
	{
		var config = new MapperConfiguration(cfg => cfg.AddProfile(new AutoMapperProfiles()));
		var mapper = new Mapper(config);

		_repository = new CustomersRepositoryMock();
		_service = new CustomersService(_repository, mapper);
	}

	[Fact]
	public async Task AddCustomerAsync_ShouldThrowBadRequestException_WhenEmailExists()
	{
		var customerDto = new AddCustomerDto
		{
			FirstName = "John",
			LastName = "Doe",
			Email = "peter.parker@example.com", // this email already exists
			PhoneNumber = "+9876543210"
		};

		var exception = await Assert.ThrowsAsync<BadRequestException>(() => _service.AddCustomerAsync(customerDto));
		Assert.Equal("A user with the specified email already exists.", exception.Message);
	}

	[Fact]
	public async Task AddCustomerAsync_ShouldThrowBadRequestException_WhenPhoneNumberExists()
	{
		var customerDto = new AddCustomerDto
		{
			FirstName = "John",
			LastName = "Doe",
			Email = "john.doe@example.com",
			PhoneNumber = "+1234567890" // this phone number already exists
		};

		var exception = await Assert.ThrowsAsync<BadRequestException>(() => _service.AddCustomerAsync(customerDto));
		Assert.Equal("A user with the specified phone number already exists.", exception.Message);
	}

	[Fact]
	public async Task AddCustomerAsync_ShouldAddCustomer_WhenValidData()
	{
		var customerDto = new AddCustomerDto
		{
			FirstName = "John",
			LastName = "Doe",
			Email = "john.doe@example.com",
			PhoneNumber = "+9876543210"
		};

		await _service.AddCustomerAsync(customerDto);

		var customer = await _repository.GetCustomerByEmailAsync(customerDto.Email);
		Assert.NotNull(customer);
		Assert.Equal(customerDto.Email, customer?.Email);
		Assert.Equal(customerDto.PhoneNumber, customer?.PhoneNumber);
	}

	[Fact]
	public async Task GetAllCustomersAsync_ShouldReturnAllCustomers()
	{
		var customers = await _service.GetAllCustomersAsync();

		Assert.NotEmpty(customers);
		Assert.Contains(customers, c => c.Email == "peter.parker@example.com");
		Assert.Contains(customers, c => c.Email == "mary.jane@example.com");
	}

	[Fact]
	public async Task AddCustomerAsync_ShouldAddCustomer_WhenAddressIsComplete()
	{
		var customerDto = new AddCustomerDto
		{
			FirstName = "John",
			LastName = "Doe",
			Email = "john.doe@example.com",
			PhoneNumber = "+9876543210",
			Address = new AddressDto
			{
				Street = "123 Elm Street",
				City = "Metropolis",
				Country = "USA"
			}
		};

		await _service.AddCustomerAsync(customerDto);

		var customer = await _repository.GetCustomerByEmailAsync(customerDto.Email);
		Assert.NotNull(customer);
		Assert.Equal(customerDto.Address.Street, customer?.Address?.Street);
		Assert.Equal(customerDto.Address.City, customer?.Address?.City);
		Assert.Equal(customerDto.Address.Country, customer?.Address?.Country);
	}

	[Fact]
	public void AddCustomerDto_ShouldFail_WhenCityIsMissingInAddress()
	{
		var addCustomerDto = new AddCustomerDto
		{
			FirstName = "John",
			LastName = "Doe",
			Email = "john.doe@example.com",
			PhoneNumber = "+1234567890",
			Address = new AddressDto
			{
				Street = "123 Main St",
				Country = "USA"
			}
		};

		var validationResults = new List<ValidationResult>();
		var isValid = Validator.TryValidateObject(addCustomerDto.Address, new ValidationContext(addCustomerDto.Address), validationResults, true);

		Assert.False(isValid);
		Assert.True(validationResults.Any(vr => vr.ErrorMessage.Contains("City is required")));
	}
}