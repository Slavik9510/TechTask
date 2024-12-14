using System.ComponentModel.DataAnnotations;

namespace TechTask.DTO;

// Dto for representing a customer's address
// Validates input data to ensure it meets required format and constraints
public record AddressDto
{
	[Required(ErrorMessage = "Street is required.")]
	[MinLength(3, ErrorMessage = "Street must be at least 3 characters long.")]
	public string Street { get; init; }

	[Required(ErrorMessage = "City is required.")]
	[MinLength(3, ErrorMessage = "City must be at least 3 characters long.")]
	public string City { get; init; }

	[Required(ErrorMessage = "Country is required.")]
	[MinLength(4, ErrorMessage = "Country must be at least 4 characters long.")]
	public string Country { get; init; }
}