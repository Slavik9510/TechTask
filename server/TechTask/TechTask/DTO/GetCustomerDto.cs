namespace TechTask.DTO;

// Dto for retrieving customer information
public class GetCustomerDto
{
	public string FirstName { get; init; }
	public string LastName { get; init; }
	public string Email { get; init; }
	public string PhoneNumber { get; init; }

	// Optional address details for the customer
	public AddressDto? Address { get; init; }
}
