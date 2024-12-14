using Microsoft.AspNetCore.Mvc;
using TechTask.DTO;
using TechTask.Interfaces;

namespace TechTask.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : Controller
{
	private readonly ICustomersService _customersService; // contains business logic related to customers

	public CustomersController(ICustomersService customersService)
	{
		_customersService = customersService;
	}

	[HttpPost]
	public async Task<IActionResult> AddCustomer(AddCustomerDto customerDto)
	{
		// add a new customer asynchronously
		await _customersService.AddCustomerAsync(customerDto);
		return Ok();
	}

	[HttpGet]
	public async Task<IActionResult> GetAllCustomers()
	{
		// retrieve all customers data asynchronously
		var dto = await _customersService.GetAllCustomersAsync();
		return Ok(dto);
	}
}
