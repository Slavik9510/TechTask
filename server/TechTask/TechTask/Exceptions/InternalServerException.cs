namespace TechTask.Exceptions;

// Represents exception for internal server errors (HTTP 500)
// Provides optional details for additional error context
public class InternalServerException : Exception
{
	public string? Details { get; set; }

	public InternalServerException(string message)
		: base(message)
	{ }

	public InternalServerException(string message, string details)
		: base(message)
	{
		Details = details;
	}
}
