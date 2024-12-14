using Microsoft.AspNetCore.Mvc;
using TechTask.Controllers;
using TechTask.DTO;
using TechTask.Interfaces;
using TechTask.Tests.Mock;

namespace TechTask.Tests;

public class CustomersControllerTests
{
	private readonly ICustomersService _customersServiceMock;
	private readonly CustomersController _controller;

	public CustomersControllerTests()
	{
		_customersServiceMock = new CustomersServiceMock();
		_controller = new CustomersController(_customersServiceMock);
	}

	[Fact]
	public async Task AddCustomer_ShouldReturnOk()
	{
		var newCustomer = new AddCustomerDto
		{
			FirstName = "Bruce",
			LastName = "Wayne",
			Email = "bruce.wayne@example.com",
			PhoneNumber = "+1234567892"
		};

		var result = await _controller.AddCustomer(newCustomer);

		var okResult = Assert.IsType<OkResult>(result);
		Assert.Equal(200, okResult.StatusCode);

		var customers = await _customersServiceMock.GetAllCustomersAsync();
		Assert.Contains(customers, c => c.Email == newCustomer.Email);
	}

	[Fact]
	public async Task GetAllCustomers_ShouldReturnOkWithCustomerList()
	{
		var result = await _controller.GetAllCustomers();

		var okResult = Assert.IsType<OkObjectResult>(result);
		Assert.Equal(200, okResult.StatusCode);

		var customers = Assert.IsAssignableFrom<IEnumerable<GetCustomerDto>>(okResult.Value);
		Assert.NotNull(customers);
		Assert.Equal(2, customers.Count());
		Assert.Contains(customers, c => c.Email == "peter.parker@example.com");
		Assert.Contains(customers, c => c.Email == "mary.jane@example.com");
	}

	[Fact]
	public async Task AddCustomer_ShouldIncreaseCustomerCount()
	{
		var newCustomer = new AddCustomerDto
		{
			FirstName = "Tony",
			LastName = "Stark",
			Email = "tony.stark@example.com",
			PhoneNumber = "+1234567893"
		};

		var initialCount = (await _customersServiceMock.GetAllCustomersAsync()).Count();

		await _controller.AddCustomer(newCustomer);

		var updatedCount = (await _customersServiceMock.GetAllCustomersAsync()).Count();
		Assert.Equal(initialCount + 1, updatedCount);
	}

}