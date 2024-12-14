namespace TechTask.Exceptions;

// Represents exception for bad requests (HTTP 400)
// Provides optional details for additional error context
public class BadRequestException : Exception
{
	public string? Details { get; set; }

	public BadRequestException(string message)
		: base(message)
	{ }

	public BadRequestException(string message, string details)
		: base(message)
	{
		Details = details;
	}
}
