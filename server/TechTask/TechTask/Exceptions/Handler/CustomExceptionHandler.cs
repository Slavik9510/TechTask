using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace TechTask.Exceptions.Handler;

// Custom exception handler for managing and logging application-level exceptions
public class CustomExceptionHandler
	(ILogger<CustomExceptionHandler> logger)
	: IExceptionHandler
{
	// Attempts to handle exception and generate a custom error response
	// Logs error and sends a structured ProblemDetails response to the client
	public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception,
		CancellationToken cancellationToken)
	{
		logger.LogError($"Error Message: {exception.Message}, Time of occurrence {DateTime.UtcNow}");

		// Define response details based on exception type
		(string Detail, string Title, int StatusCode) details = exception switch
		{
			InternalServerException =>
			(
				exception.Message,
				exception.GetType().Name,
				context.Response.StatusCode = StatusCodes.Status500InternalServerError
			),
			BadRequestException =>
			(
				exception.Message,
				exception.GetType().Name,
				context.Response.StatusCode = StatusCodes.Status400BadRequest
			),
			// Treats unknown exceptions as internal server errors
			_ =>
			(
				exception.Message,
				exception.GetType().Name,
				context.Response.StatusCode = StatusCodes.Status500InternalServerError
			)
		};

		// Creating object to structure error response
		var problemDetails = new ProblemDetails
		{
			Title = details.Title,
			Detail = details.Detail,
			Status = details.StatusCode
		};

		// Writing problemDetails object as a json response to client
		await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
		return true; // return true to indicate exception was handled
	}
}
