namespace TechTask.DTO;

using System.ComponentModel.DataAnnotations;

// Dto for adding customer
// Validates input data to ensure it meets required format and constraints
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

	// Phone number validation according to the E.164 standard
	[Required(ErrorMessage = "Phone number is required.")]
	[RegularExpression("""^\+[1-9]\d{1,14}$""", ErrorMessage = "Invalid phone number format.")]
	public string PhoneNumber { get; init; }

	// Optional address details for the customer
	public AddressDto? Address { get; init; } // Optional
}
