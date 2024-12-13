using Microsoft.AspNetCore.Mvc;
using TechTask.DTO;
using TechTask.Interfaces;

namespace TechTask.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : Controller
{
	private readonly ICustomersService _customersService;

	public CustomersController(ICustomersService customersService)
	{
		_customersService = customersService;
	}

	[HttpPost]
	public async Task<IActionResult> AddCustomer(AddCustomerDto customerDto)
	{
		await _customersService.AddCustomerAsync(customerDto);
		return Ok();
	}

	[HttpGet]
	public async Task<IActionResult> GetAllCustomers()
	{
		var dto = await _customersService.GetAllCustomersAsync();
		return Ok(dto);
	}
}
