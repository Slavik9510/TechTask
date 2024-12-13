namespace TechTask.DTO;

public class GetCustomerDto
{
	public string FirstName { get; init; }
	public string LastName { get; init; }
	public string Email { get; init; }
	public string PhoneNumber { get; init; }
	public string? Address { get; init; }
}
