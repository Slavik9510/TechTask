namespace TechTask.DTO;

using System.ComponentModel.DataAnnotations;

public record AddCustomerDto
{
	[Required(ErrorMessage = "First name is required.")]
	[StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
	public string FirstName { get; init; }

	[Required(ErrorMessage = "Last name is required.")]
	[StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
	public string LastName { get; init; }

	[Required(ErrorMessage = "Email is required.")]
	[EmailAddress(ErrorMessage = "Invalid email address format.")]
	public string Email { get; init; }

	[Required(ErrorMessage = "Phone number is required.")]
	[RegularExpression("""^\+380\d{9}$""", ErrorMessage = "Invalid phone number format.")]
	public string PhoneNumber { get; init; }

	[StringLength(200, ErrorMessage = "Address cannot exceed 200 characters.")]
	public string? Address { get; init; } // Optional
}
