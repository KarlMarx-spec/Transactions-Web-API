using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Transactions_Web_API.Exceptions;

namespace Transactions_Web_API.Middlewares
{
	/// <summary>
	/// Middleware обработки исключений
	/// </summary>
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate _next;

		public ExceptionMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(
			HttpContext httpContext)
		{
			try
			{
				await _next(httpContext);
			}
			catch (BaseException ex)
			{
				await HandleExceptionAsync(httpContext, ex);
			}
		}

		private static async Task HandleExceptionAsync(
			HttpContext httpContext,
			BaseException exception)
		{
			httpContext.Response.StatusCode = GetStatusCode(exception);

			httpContext.Response.ContentType = "application/json";

			var activity = httpContext.Features.Get<IHttpActivityFeature>()?.Activity;

			var problemDetails = new ProblemDetails
			{
				Status = GetStatusCode(exception),
				Title = exception.Title,
				Type = exception.GetType().Name,
				Detail = exception.Details,
				Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}",
				Extensions = new Dictionary<string, object?>
				{
					{ "requestId", httpContext.TraceIdentifier },
					{ "traceId", activity?.Id }
				}
			};

			await httpContext.Response.WriteAsJsonAsync(problemDetails);
		}

		private static int GetStatusCode(BaseException exception)
		{
			return exception switch
			{
				BusinessLogicException businessLogicException => (int)businessLogicException.StatusCode,
				EntityNotFoundException entityNotFoundException => (int)entityNotFoundException.StatusCode,
				_ => StatusCodes.Status500InternalServerError
			};
		}
	}
}
