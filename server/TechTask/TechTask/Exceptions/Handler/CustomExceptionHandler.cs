using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace TechTask.Exceptions.Handler;

public class CustomExceptionHandler
	(ILogger<CustomExceptionHandler> logger)
	: IExceptionHandler
{
	public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception,
		CancellationToken cancellationToken)
	{
		logger.LogError($"Error Message: {exception.Message}, Time of occurrence {DateTime.UtcNow}");

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
			OperationCanceledException => (
				"Request was canceled by the client.",
				exception.GetType().Name,
				context.Response.StatusCode = StatusCodes.Status499ClientClosedRequest
			),
			_ =>
			(
				exception.Message,
				exception.GetType().Name,
				context.Response.StatusCode = StatusCodes.Status500InternalServerError
			)
		};

		var problemDetails = new ProblemDetails
		{
			Title = details.Title,
			Detail = details.Detail,
			Status = details.StatusCode
		};

		await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
		return true;
	}
}
