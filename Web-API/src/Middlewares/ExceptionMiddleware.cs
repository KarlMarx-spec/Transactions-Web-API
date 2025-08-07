using Domain.Exceptions;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace Transactions_Web_API.src.Middlewares
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

		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await _next(httpContext);
			}
			catch (BaseBusinessException ex)
			{
				await HandleExceptionAsync(httpContext, ex);
			}
		}

		private static async Task HandleExceptionAsync(
			HttpContext httpContext,
			BaseBusinessException exception)
		{
			httpContext.Response.StatusCode = (int)exception.StatusCode;

			httpContext.Response.ContentType = "application/json";

			var activity = httpContext.Features.Get<IHttpActivityFeature>()?.Activity;

			var problemDetails = new ProblemDetails
			{
				Status = (int)exception.StatusCode,
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
	}
}
